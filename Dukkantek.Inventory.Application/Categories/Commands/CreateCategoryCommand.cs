using MediatR;
using Dukkantek.Inventory.Application.Categories.Models.Result;

namespace Dukkantek.Inventory.Application.Categories.Commands
{
     public class CreateCategoryCommand : IRequest<GetCategoryQueryResult>
     {
          public string Name { get; set; }
     }
}