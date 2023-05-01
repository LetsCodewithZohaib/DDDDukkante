using Dukkantek.Inventory.Domain.Products;
using MediatR;

namespace Dukkantek.Inventory.Domain.Events.Products
{
    public class ProductStatusChangedEvent : INotification
    {
        public Product Product { get; }
        public ProductStatusChangedEvent(Product product)
        {
            Product = product;
        }
    }
}