using Dukkantek.Inventory.Application.Products.Models.Results;
using MediatR;
using System;

namespace Dukkantek.Inventory.Application.Products.Commands
{
    public class SellProductCommand : IRequest<GetProductQueryResult>
    {
        public Guid Id { get; set; }
    }
}
