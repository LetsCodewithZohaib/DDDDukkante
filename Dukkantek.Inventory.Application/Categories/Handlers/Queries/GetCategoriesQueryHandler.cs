using AutoMapper;
using Dukkantek.Inventory.Application.Categories.Models.Result;
using Dukkantek.Inventory.Application.Categories.Queries;
using Dukkantek.Inventory.Application.Interfaces.Persistence;
using Dukkantek.Inventory.Domain.Events.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Categories.Handlers.Queries
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<GetCategoryQueryResult>>
    {
        private readonly IMediator _mediator;
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IMediator mediator, IAppDbContext context, IMapper mapper)
        {
            _mediator = mediator;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetCategoryQueryResult>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.Include(e => e.Products).ToListAsync(cancellationToken);
            await _mediator.Publish(new AllCategoriesViewedEvent(categories), cancellationToken);
            return _mapper.Map<IEnumerable<GetCategoryQueryResult>>(categories);
        }
    }
}