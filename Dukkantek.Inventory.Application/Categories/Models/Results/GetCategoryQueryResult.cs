using System;

namespace Dukkantek.Inventory.Application.Categories.Models.Result
{
     public class GetCategoryQueryResult
     {
          public Guid Id { get; set; }
          public string Name { get; set; }
          public int Products { get; set; }
     }
}