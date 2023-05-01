using System;
using Dukkantek.Inventory.Application.Products.Models.Results;
using Dukkantek.Inventory.Common.Enums;
using MediatR;

namespace Dukkantek.Inventory.Application.Products.Commands
{
    public class ChangeProductStatusCommand : IRequest<GetProductQueryResult>
    {
        public Guid Id { get; set; }
        public ProductStatusEnum Status { get; set; }
    }
}
