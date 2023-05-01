using Dukkantek.Inventory.Domain.Events.Products;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Products.Handlers.Events
{
    public class ProductViewedEventHandler : INotificationHandler<ProductViewedEvent>
    {
        private readonly ILogger<ProductViewedEventHandler> _logger;

        public ProductViewedEventHandler(ILogger<ProductViewedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProductViewedEvent notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.Product.DomainEvents;
            _logger.LogInformation($"Product viewed event {domainEvent.GetType().Name} occur for product with Id: {notification.Product.Id} at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
