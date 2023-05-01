using Dukkantek.Inventory.Domain.Categories;
using MediatR;

namespace Dukkantek.Inventory.Domain.Events.Categories
{
    public class CategoryCreatedEvent : INotification
    {
        public Category Category { get; }

        public CategoryCreatedEvent(Category category)
        {
            Category = category;
        }
    }
}