using Dukkantek.Inventory.Domain.Events.Categories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Categories.Handlers.Events
{
    public class CategoryDeletedEventHandler : INotificationHandler<CategoryDeletedEvent>
    {
        private readonly ILogger<CategoryDeletedEventHandler> _logger;

        public CategoryDeletedEventHandler(ILogger<CategoryDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.Category.DomainEvents;
            _logger.LogInformation($"Category deleted event {domainEvent.GetType().Name} occur for category with Id: {notification.Category.Id} at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
