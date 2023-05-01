using AutoMapper;
using Dukkantek.Inventory.Application.Interfaces.Persistence;
using Dukkantek.Inventory.Application.Products.Models.Results;
using Dukkantek.Inventory.Application.Products.Queries;
using Dukkantek.Inventory.Domain.Events.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Products.Handlers.Queries
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductQueryResult>
    {
        private readonly IMediator _mediator;
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IMediator mediator, IAppDbContext context, IMapper mapper)
        {
            _mediator = mediator;
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetProductQueryResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.Include(e => e.Category).SingleAsync(e => e.Id.Equals(request.ProductId), cancellationToken);
            await _mediator.Publish(new ProductViewedEvent(product), cancellationToken);
            return _mapper.Map<GetProductQueryResult>(product);
        }
    }
}
