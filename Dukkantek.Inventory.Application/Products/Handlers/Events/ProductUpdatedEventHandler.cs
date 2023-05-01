using Dukkantek.Inventory.Domain.Events.Products;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Products.Handlers.Events
{
    public class ProductUpdatedEventHandler : INotificationHandler<ProductUpdatedEvent>
    {
        private readonly ILogger<ProductUpdatedEventHandler> _logger;

        public ProductUpdatedEventHandler(ILogger<ProductUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.Product.DomainEvents;
            _logger.LogInformation($"Product updated event {domainEvent.GetType().Name} occur for product with Id: {notification.Product.Id} at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
