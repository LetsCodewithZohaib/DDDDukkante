using System.Collections.Generic;

namespace Dukkantek.Inventory.Domain.Common
{
     public interface IHasDomainEvent
     {
          public List<DomainEvent> DomainEvents { get; set; }
     }
}