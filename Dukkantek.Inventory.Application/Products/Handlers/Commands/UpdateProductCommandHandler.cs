using AutoMapper;
using Dukkantek.Inventory.Application.Interfaces.Persistence;
using Dukkantek.Inventory.Application.Products.Commands;
using Dukkantek.Inventory.Application.Products.Models.Results;
using Dukkantek.Inventory.Domain.Events.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Products.Handlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, GetProductQueryResult>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateProductCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<GetProductQueryResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productFromDb = await _context.Products.Include(e => e.Category).SingleAsync(e => e.Id.Equals(request.Id), cancellationToken);
            var product = _mapper.Map(request, productFromDb);
            product.AddDomainEvent(new ProductUpdatedEvent(product));

            _context.Products.Attach(product);
            await _context.SaveChangesAsync(cancellationToken);
            await _mediator.Publish(new ProductUpdatedEvent(product), cancellationToken);

            return _mapper.Map(product, new GetProductQueryResult());
        }
    }
}
