using Dukkantek.Inventory.Domain.Categories;
using MediatR;

namespace Dukkantek.Inventory.Domain.Events.Categories
{
    public class CategoryDeletedEvent : INotification
    {
        public Category Category { get; }

        public CategoryDeletedEvent(Category category)
        {
            Category = category;
        }
    }
}
