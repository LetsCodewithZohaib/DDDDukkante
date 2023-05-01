using Dukkantek.Inventory.Domain.Products;
using MediatR;

namespace Dukkantek.Inventory.Domain.Events.Products
{
    public class ProductSoldEvent : INotification
    {
        public Product Product { get; }

        public ProductSoldEvent(Product product)
        {
            Product = product;
        }
    }
}