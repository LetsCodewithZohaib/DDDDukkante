using Dukkantek.Inventory.Domain.Events.Products;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Products.Handlers.Events
{
    public class ProductSoldEventHandler : INotificationHandler<ProductSoldEvent>
    {
        private readonly ILogger<ProductSoldEventHandler> _logger;

        public ProductSoldEventHandler(ILogger<ProductSoldEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProductSoldEvent notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.Product.DomainEvents;
            _logger.LogInformation($"Product sold event {domainEvent.GetType().Name} occur for product with Id: {notification.Product.Id} at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
