using AutoMapper;
using Dukkantek.Inventory.Application.Interfaces.Persistence;
using Dukkantek.Inventory.Application.Products.Models.Results;
using Dukkantek.Inventory.Application.Products.Queries;
using Dukkantek.Inventory.Common.Enums;
using Dukkantek.Inventory.Domain.Events.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Products.Handlers.Queries
{
    public class GetProductCountQueryHandler : IRequestHandler<GetProductCountQuery, GetProductCountQueryResult>
    {
        private readonly IMediator _mediator;
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetProductCountQueryHandler(IMediator mediator, IAppDbContext context, IMapper mapper)
        {
            _mediator = mediator;
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetProductCountQueryResult> Handle(GetProductCountQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products.Include(e => e.Category).ToListAsync(cancellationToken);
            await _mediator.Publish(new AllProductsViewedEvent(products), cancellationToken);

            return new GetProductCountQueryResult
            {
                Damaged = products.Count(e => e.Status == ProductStatusEnum.Damaged),
                InStock = products.Count(e => e.Status == ProductStatusEnum.InStock),
                Sold = products.Count(e => e.Status == ProductStatusEnum.Sold)
            };
        }
    }
}
