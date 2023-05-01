using Dukkantek.Inventory.Domain.Categories;
using MediatR;
using System.Collections.Generic;

namespace Dukkantek.Inventory.Domain.Events.Categories
{
    public class AllCategoriesViewedEvent : INotification
    {
        public List<Category> Categories { get; }

        public AllCategoriesViewedEvent(List<Category> categories)
        {
            Categories = categories;
        }
    }
}
