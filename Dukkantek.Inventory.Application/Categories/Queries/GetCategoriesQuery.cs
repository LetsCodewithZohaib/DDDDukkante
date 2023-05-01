using MediatR;
using Dukkantek.Inventory.Application.Categories.Models.Result;
using System.Collections.Generic;

namespace Dukkantek.Inventory.Application.Categories.Queries
{
     public class GetCategoriesQuery : IRequest<IEnumerable<GetCategoryQueryResult>>
     {
     }
}