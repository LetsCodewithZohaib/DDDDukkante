using Dukkantek.Inventory.Application.Categories.Commands;
using Dukkantek.Inventory.Application.Common.Models.Results;
using Dukkantek.Inventory.Application.Interfaces.Persistence;
using Dukkantek.Inventory.Domain.Events.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Categories.Handlers.Commands
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeleteCommandResult>
    {
        private readonly IAppDbContext _context;
        private readonly IMediator _mediator;

        public DeleteCategoryCommandHandler(IAppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<DeleteCommandResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.SingleAsync(e => e.Id.Equals(request.Id), cancellationToken);

            category.AddDomainEvent(new CategoryDeletedEvent(category));
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);
            await _mediator.Publish(new CategoryDeletedEvent(category), cancellationToken);

            return new DeleteCommandResult
            {
                Id = category.Id,
                IsDeleted = true,
                Message = $"Category with id {category.Id} is successfully deleted"
            };
        }
    }
}