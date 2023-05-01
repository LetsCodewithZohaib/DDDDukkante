using AutoMapper;
using Dukkantek.Inventory.Application.Interfaces.Persistence;
using Dukkantek.Inventory.Application.Products.Models.Results;
using Dukkantek.Inventory.Application.Products.Queries;
using Dukkantek.Inventory.Domain.Events.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Products.Handlers.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<GetProductQueryResult>>
    {
        private readonly IMediator _mediator;
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IMediator mediator, IAppDbContext context, IMapper mapper)
        {
            _mediator = mediator;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetProductQueryResult>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products.Include(e => e.Category).ToListAsync(cancellationToken);
            await _mediator.Publish(new AllProductsViewedEvent(products), cancellationToken);
            return _mapper.Map<IEnumerable<GetProductQueryResult>>(products);
        }
    }
}
