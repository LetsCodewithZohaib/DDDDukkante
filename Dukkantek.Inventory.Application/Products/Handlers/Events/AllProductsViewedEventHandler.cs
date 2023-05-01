using Dukkantek.Inventory.Domain.Events.Products;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Products.Handlers.Events
{
    public class AllProductsViewedEventHandler : INotificationHandler<AllProductsViewedEvent>
    {
        private readonly ILogger<AllProductsViewedEventHandler> _logger;

        public AllProductsViewedEventHandler(ILogger<AllProductsViewedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(AllProductsViewedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"All products accessed event occur at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
