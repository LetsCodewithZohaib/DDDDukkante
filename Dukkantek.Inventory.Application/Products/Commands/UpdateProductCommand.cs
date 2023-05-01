using Dukkantek.Inventory.Application.Products.Models.Results;
using Dukkantek.Inventory.Common.Enums;
using MediatR;
using System;

namespace Dukkantek.Inventory.Application.Products.Commands
{
    public class UpdateProductCommand : IRequest<GetProductQueryResult>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BarCode { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public ProductStatusEnum Status { get; set; }
        public Guid CategoryId { get; set; }
    }
}
