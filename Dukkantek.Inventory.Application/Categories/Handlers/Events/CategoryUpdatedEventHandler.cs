using Dukkantek.Inventory.Domain.Events.Categories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Categories.Handlers.Events
{
    public class CategoryUpdatedEventHandler : INotificationHandler<CategoryUpdatedEvent>
    {
        private readonly ILogger<CategoryUpdatedEventHandler> _logger;

        public CategoryUpdatedEventHandler(ILogger<CategoryUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CategoryUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.Category.DomainEvents;
            _logger.LogInformation($"Category updated event {domainEvent.GetType().Name} occur for category with Id: {notification.Category.Id} at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
