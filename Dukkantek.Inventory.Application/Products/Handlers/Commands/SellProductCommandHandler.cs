using AutoMapper;
using Dukkantek.Inventory.Application.Interfaces.Persistence;
using Dukkantek.Inventory.Application.Products.Commands;
using Dukkantek.Inventory.Application.Products.Models.Results;
using Dukkantek.Inventory.Domain.Events.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Dukkantek.Inventory.Common.Enums;

namespace Dukkantek.Inventory.Application.Products.Handlers.Commands
{
    public class SellProductCommandHandler : IRequestHandler<SellProductCommand, GetProductQueryResult>
    {
        private readonly IAppDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SellProductCommandHandler(IAppDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<GetProductQueryResult> Handle(SellProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.Include(e => e.Category).SingleAsync(e => e.Id.Equals(request.Id), cancellationToken);
            product.Status = ProductStatusEnum.Sold;

            product.AddDomainEvent(new ProductSoldEvent(product));

            _context.Products.Attach(product);
            await _context.SaveChangesAsync(cancellationToken);
            await _mediator.Publish(new ProductUpdatedEvent(product), cancellationToken);

            return _mapper.Map(product, new GetProductQueryResult());
        }
    }
}
