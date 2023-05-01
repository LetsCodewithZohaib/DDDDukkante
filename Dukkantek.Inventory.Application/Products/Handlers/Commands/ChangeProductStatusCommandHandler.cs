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
    public class ChangeProductStatusCommandHandler : IRequestHandler<ChangeProductStatusCommand, GetProductQueryResult>
    {
        private readonly IAppDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ChangeProductStatusCommandHandler(IAppDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<GetProductQueryResult> Handle(ChangeProductStatusCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.Include(e => e.Category).SingleAsync(e => e.Id.Equals(request.Id), cancellationToken);
            product.Status = request.Status;

            product.AddDomainEvent(new ProductStatusChangedEvent(product));

            _context.Products.Attach(product);
            await _context.SaveChangesAsync(cancellationToken);
            await _mediator.Publish(new ProductStatusChangedEvent(product), cancellationToken);

            return _mapper.Map(product, new GetProductQueryResult());
        }
    }
}
