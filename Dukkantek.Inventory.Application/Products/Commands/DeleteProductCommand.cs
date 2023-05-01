using Dukkantek.Inventory.Application.Common.Models.Results;
using MediatR;
using System;

namespace Dukkantek.Inventory.Application.Products.Commands
{
    public class DeleteProductCommand : IRequest<DeleteCommandResult>
    {
        public Guid Id { get; set; }
    }
}