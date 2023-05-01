using AutoMapper;
using Dukkantek.Inventory.Application.Categories.Commands;
using Dukkantek.Inventory.Application.Categories.Models.Result;
using Dukkantek.Inventory.Application.Interfaces.Persistence;
using Dukkantek.Inventory.Domain.Events.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Categories.Handlers.Commands
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, GetCategoryQueryResult>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateCategoryCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<GetCategoryQueryResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryFromDb = await _context.Categories.SingleAsync(e => e.Id.Equals(request.Id), cancellationToken);
            var category = _mapper.Map(request, categoryFromDb);
            category.AddDomainEvent(new CategoryUpdatedEvent(category));

            _context.Categories.Attach(category);
            await _context.SaveChangesAsync(cancellationToken);
            await _mediator.Publish(new CategoryUpdatedEvent(category), cancellationToken);

            return _mapper.Map(category, new GetCategoryQueryResult());
        }
    }
}