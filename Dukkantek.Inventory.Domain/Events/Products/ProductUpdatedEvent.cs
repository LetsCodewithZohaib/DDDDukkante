using Dukkantek.Inventory.Domain.Products;
using MediatR;

namespace Dukkantek.Inventory.Domain.Events.Products
{
    public class ProductUpdatedEvent : INotification
    {
        public Product Product { get; }

        public ProductUpdatedEvent(Product product)
        {
            Product = product;
        }
    }
}
