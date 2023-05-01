using Dukkantek.Inventory.Application.Products.Models.Results;
using MediatR;
using System;

namespace Dukkantek.Inventory.Application.Products.Queries
{
    public class GetProductQuery : IRequest<GetProductQueryResult>
    {
        public Guid ProductId { get; set; }
    }
}
