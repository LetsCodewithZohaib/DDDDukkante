using Dukkantek.Inventory.Domain.Products;
using MediatR;

namespace Dukkantek.Inventory.Domain.Events.Products
{
    public class ProductDeletedEvent : INotification
    {
        public Product Product { get; }

        public ProductDeletedEvent(Product product)
        {
            Product = product;
        }
    }
}