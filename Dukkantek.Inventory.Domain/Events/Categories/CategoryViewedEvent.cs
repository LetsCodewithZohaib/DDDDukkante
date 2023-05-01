using Dukkantek.Inventory.Domain.Categories;
using MediatR;

namespace Dukkantek.Inventory.Domain.Events.Categories
{
    public class CategoryViewedEvent : INotification
    {
        public Category Category { get; }

        public CategoryViewedEvent(Category category)
        {
            Category = category;
        }
    }
}