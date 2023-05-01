using AutoMapper;
using Dukkantek.Inventory.Application.Categories.Commands;
using Dukkantek.Inventory.Application.Categories.Models.Result;
using Dukkantek.Inventory.Application.Interfaces.Persistence;
using Dukkantek.Inventory.Domain.Categories;
using Dukkantek.Inventory.Domain.Events.Categories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Categories.Handlers.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, GetCategoryQueryResult>
    {
        private readonly IAppDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IAppDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<GetCategoryQueryResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            category.AddDomainEvent(new CategoryCreatedEvent(category));

            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            await _mediator.Publish(new CategoryCreatedEvent(category), cancellationToken);

            return _mapper.Map(category, new GetCategoryQueryResult());
        }
    }
}