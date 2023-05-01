using Dukkantek.Inventory.Application.Products.Models.Results;
using MediatR;
using System;
using Dukkantek.Inventory.Common.Enums;

namespace Dukkantek.Inventory.Application.Products.Queries
{
    public class GetProductCountQuery : IRequest<GetProductCountQueryResult>
    {
    }
}
