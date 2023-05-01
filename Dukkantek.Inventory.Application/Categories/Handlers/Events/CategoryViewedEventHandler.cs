using Dukkantek.Inventory.Domain.Events.Categories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Categories.Handlers.Events
{
    public class CategoryViewedEventHandler : INotificationHandler<CategoryViewedEvent>
    {
        private readonly ILogger<CategoryViewedEventHandler> _logger;

        public CategoryViewedEventHandler(ILogger<CategoryViewedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CategoryViewedEvent notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.Category.DomainEvents;
            _logger.LogInformation($"Category viewed event {domainEvent.GetType().Name} occur for category with Id: {notification.Category.Id} at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
