using Dukkantek.Inventory.Domain.Categories;
using MediatR;

namespace Dukkantek.Inventory.Domain.Events.Categories
{
    public class CategoryUpdatedEvent : INotification
    {
        public Category Category { get; }

        public CategoryUpdatedEvent(Category category)
        {
            Category = category;
        }
    }
}
