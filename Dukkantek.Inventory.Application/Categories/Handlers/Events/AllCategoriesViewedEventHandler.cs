using Dukkantek.Inventory.Domain.Events.Categories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Categories.Handlers.Events
{
    public class AllCategoriesViewedEventHandler : INotificationHandler<AllCategoriesViewedEvent>
    {
        private readonly ILogger<AllCategoriesViewedEventHandler> _logger;

        public AllCategoriesViewedEventHandler(ILogger<AllCategoriesViewedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(AllCategoriesViewedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"All categories accessed event occur at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
