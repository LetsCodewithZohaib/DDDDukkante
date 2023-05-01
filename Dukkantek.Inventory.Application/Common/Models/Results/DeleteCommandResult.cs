using System;

namespace Dukkantek.Inventory.Application.Common.Models.Results
{
     public class DeleteCommandResult
     {
          public Guid Id { get; set; }
          public bool IsDeleted { get; set; }
          public string Message { get; set; }
     }
}