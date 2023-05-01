using AutoMapper;
using Dukkantek.Inventory.Application.Categories.Models.Result;
using Dukkantek.Inventory.Application.Categories.Queries;
using Dukkantek.Inventory.Application.Interfaces.Persistence;
using Dukkantek.Inventory.Domain.Events.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Categories.Handlers.Queries
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, GetCategoryQueryResult>
    {
        private readonly IMediator _mediator;
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IMediator mediator, IAppDbContext context, IMapper mapper)
        {
            _mediator = mediator;
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetCategoryQueryResult> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.Include(e => e.Products).SingleAsync(e => e.Id.Equals(request.Id), cancellationToken);
            await _mediator.Publish(new CategoryViewedEvent(category), cancellationToken);
            return _mapper.Map<GetCategoryQueryResult>(category);
        }
    }
}