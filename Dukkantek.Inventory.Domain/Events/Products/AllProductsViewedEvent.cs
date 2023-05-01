using Dukkantek.Inventory.Domain.Products;
using MediatR;
using System.Collections.Generic;

namespace Dukkantek.Inventory.Domain.Events.Products
{
    public class AllProductsViewedEvent : INotification
    {
        public List<Product> Products { get; }

        public AllProductsViewedEvent(List<Product> products)
        {
            Products = products;
        }
    }
}
