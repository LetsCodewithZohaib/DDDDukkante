using Dukkantek.Inventory.Domain.Products;
using MediatR;

namespace Dukkantek.Inventory.Domain.Events.Products
{
    public class ProductViewedEvent : INotification
    {
        public Product Product { get; }

        public ProductViewedEvent(Product product)
        {
            Product = product;
        }
    }
}