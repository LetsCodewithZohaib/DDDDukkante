using AutoMapper;
using Dukkantek.Inventory.Application.Interfaces.Persistence;
using Dukkantek.Inventory.Application.Products.Commands;
using Dukkantek.Inventory.Application.Products.Models.Results;
using Dukkantek.Inventory.Domain.Events.Products;
using Dukkantek.Inventory.Domain.Products;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Products.Handlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, GetProductQueryResult>
    {
        private readonly IAppDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IAppDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<GetProductQueryResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            product.AddDomainEvent(new ProductCreatedEvent(product));

            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            await _mediator.Publish(new ProductCreatedEvent(product), cancellationToken);

            return _mapper.Map(product, new GetProductQueryResult());
        }
    }
}
