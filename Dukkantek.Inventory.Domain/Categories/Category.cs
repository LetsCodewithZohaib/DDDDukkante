using Dukkantek.Inventory.Domain.Common;
using Dukkantek.Inventory.Domain.Products;
using System.Collections.Generic;

namespace Dukkantek.Inventory.Domain.Categories
{
    public class Category : Entity, IHasDomainEvent
    {
        public string Name { get; set; }

        public new List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();

        // One Category to Many Relationships

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
