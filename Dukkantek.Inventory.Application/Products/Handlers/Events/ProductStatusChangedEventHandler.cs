using Dukkantek.Inventory.Domain.Events.Products;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Products.Handlers.Events
{
    public class ProductStatusChangedEventHandler : INotificationHandler<ProductStatusChangedEvent>
    {
        private readonly ILogger<ProductStatusChangedEventHandler> _logger;

        public ProductStatusChangedEventHandler(ILogger<ProductStatusChangedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProductStatusChangedEvent notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.Product.DomainEvents;
            _logger.LogInformation($"Product Status Changed event {domainEvent.GetType().Name} occur for product with Id: {notification.Product.Id} at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
