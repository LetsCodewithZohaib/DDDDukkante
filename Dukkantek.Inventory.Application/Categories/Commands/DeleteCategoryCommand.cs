using MediatR;
using Dukkantek.Inventory.Application.Common.Models.Results;
using System;

namespace Dukkantek.Inventory.Application.Categories.Commands
{
     public class DeleteCategoryCommand : IRequest<DeleteCommandResult>
     {
          public Guid Id { get; set; }
     }
}