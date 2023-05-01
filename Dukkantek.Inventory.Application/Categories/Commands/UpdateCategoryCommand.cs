using MediatR;
using Dukkantek.Inventory.Application.Categories.Models.Result;
using System;

namespace Dukkantek.Inventory.Application.Categories.Commands
{
     public class UpdateCategoryCommand : IRequest<GetCategoryQueryResult>
     {
          public Guid Id { get; set; }
          public string Name { get; set; }
          public string Description { get; set; }
     }
}