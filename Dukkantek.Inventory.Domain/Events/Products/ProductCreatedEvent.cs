using Dukkantek.Inventory.Domain.Products;
using MediatR;

namespace Dukkantek.Inventory.Domain.Events.Products
{
    public class ProductCreatedEvent : INotification
    {
        public Product Product { get; }

        public ProductCreatedEvent(Product product)
        {
            Product = product;
        }
    }
}
