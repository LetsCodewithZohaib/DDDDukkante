using Dukkantek.Inventory.Application.Common.Models.Results;
using Dukkantek.Inventory.Application.Interfaces.Persistence;
using Dukkantek.Inventory.Application.Products.Commands;
using Dukkantek.Inventory.Domain.Events.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Products.Handlers.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeleteCommandResult>
    {
        private readonly IAppDbContext _context;
        private readonly IMediator _mediator;

        public DeleteProductCommandHandler(IAppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<DeleteCommandResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.SingleAsync(e => e.Id.Equals(request.Id), cancellationToken);

            product.AddDomainEvent(new ProductDeletedEvent(product));
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
            await _mediator.Publish(new ProductDeletedEvent(product), cancellationToken);

            return new DeleteCommandResult
            {
                Id = product.Id,
                IsDeleted = true,
                Message = $"Product with id {product.Id} is successfully deleted"
            };
        }
    }
}
