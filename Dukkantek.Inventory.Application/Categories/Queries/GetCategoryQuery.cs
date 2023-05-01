using MediatR;
using Dukkantek.Inventory.Application.Categories.Models.Result;
using System;

namespace Dukkantek.Inventory.Application.Categories.Queries
{
     public class GetCategoryQuery : IRequest<GetCategoryQueryResult>
     {
          public Guid Id { get; set; }
     }
}