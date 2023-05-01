using System;
using Dukkantek.Inventory.Common.Enums;
using Dukkantek.Inventory.Domain.Common;
using System.Collections.Generic;
using Dukkantek.Inventory.Domain.Categories;

namespace Dukkantek.Inventory.Domain.Products
{
    public class Product : Entity, IHasDomainEvent
    {
        public string Name { get; set; }
        public string BarCode { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public ProductStatusEnum Status { get; set; }
        public Guid CategoryId { get; set; }

        public new List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();


        // One to Many Product Relationships
        public virtual Category Category { get; set; }
    }
}
