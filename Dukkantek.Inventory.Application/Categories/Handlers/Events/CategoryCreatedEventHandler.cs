using Dukkantek.Inventory.Domain.Events.Categories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Categories.Handlers.Events
{
    public class CategoryCreatedEventHandler : INotificationHandler<CategoryCreatedEvent>
    {
        private readonly ILogger<CategoryCreatedEventHandler> _logger;

        public CategoryCreatedEventHandler(ILogger<CategoryCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CategoryCreatedEvent notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.Category.DomainEvents;
            _logger.LogInformation($"Category created event {domainEvent.GetType().Name} occur for category with Id: {notification.Category.Id} at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
