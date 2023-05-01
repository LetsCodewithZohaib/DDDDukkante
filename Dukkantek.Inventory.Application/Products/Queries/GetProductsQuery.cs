using Dukkantek.Inventory.Application.Products.Models.Results;
using MediatR;
using System.Collections.Generic;

namespace Dukkantek.Inventory.Application.Products.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<GetProductQueryResult>>
    {
    }
}
