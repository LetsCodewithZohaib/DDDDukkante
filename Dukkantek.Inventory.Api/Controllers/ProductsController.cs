using AutoMapper;
using Dukkantek.Inventory.Api.Base;
using Dukkantek.Inventory.Application.Common.Models.Results;
using Dukkantek.Inventory.Application.Products.Commands;
using Dukkantek.Inventory.Application.Products.Models.Results;
using Dukkantek.Inventory.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Api.Controllers
{
    /// <summary>
    /// this is the products endpoints
    /// </summary>
    [Route("[controller]")]
    public class ProductsController : BaseController
    {
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IMediator mediator, IMapper mapper, ILogger<ProductsController> logger) : base(mediator, mapper)
        {
            _logger = logger;
        }

        /// <summary>
        /// gets all products in the system
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(typeof(IEnumerable<GetProductQueryResult>), StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return Ok(products);
        }

        /// <summary>
        /// gets a summary of products in the system
        /// </summary>
        /// <returns></returns>
        [HttpGet("count", Name = nameof(GetProductsCount)), ProducesResponseType(typeof(IEnumerable<GetProductCountQueryResult>), StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> GetProductsCount()
        {
            var result = await _mediator.Send(new GetProductCountQuery());
            return Ok(result);
        }

        /// <summary>
        /// sells a product
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("sellproduct", Name = nameof(SellProduct)), ProducesResponseType(typeof(GetProductQueryResult), StatusCodes.Status201Created), ProducesDefaultResponseType]
        public async Task<IActionResult> SellProduct([FromBody] SellProductCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtRoute(nameof(GetProduct), new { productid = result.Id }, result);
        }

        /// <summary>
        /// sells a product
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("changeproductstatus", Name = nameof(ChangeProductStatus)), ProducesResponseType(typeof(GetProductQueryResult), StatusCodes.Status201Created), ProducesDefaultResponseType]
        public async Task<IActionResult> ChangeProductStatus([FromBody] ChangeProductStatusCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtRoute(nameof(GetProduct), new { productid = result.Id }, result);
        }

        /// <summary>
        ///  gets a single product by ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("{productId}", Name = nameof(GetProduct)), ProducesResponseType(typeof(GetProductQueryResult), StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> GetProduct(Guid productId)
        {
            var product = await _mediator.Send(new GetProductQuery { ProductId = productId });
            return Ok(product);
        }


        /// <summary>
        /// creates a new product
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost(Name = nameof(CreateProduct)), ProducesResponseType(typeof(GetProductQueryResult), StatusCodes.Status201Created), ProducesDefaultResponseType]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtRoute(nameof(GetProduct), new { productid = result.Id }, result);
        }

        /// <summary>
        /// updates an product
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut(Name = nameof(UpdateProduct)), ProducesResponseType(typeof(GetProductQueryResult), StatusCodes.Status201Created), ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return AcceptedAtRoute(nameof(GetProduct), new { productid = result.Id }, result);
        }

        /// <summary>
        /// deletes single product
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete(Name = nameof(DeleteProduct)), ProducesResponseType(typeof(DeleteCommandResult), StatusCodes.Status201Created), ProducesResponseType(typeof(string), StatusCodes.Status404NotFound), ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteProduct([FromBody] DeleteProductCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsDeleted)
            {
                return NotFound($"{result.Message}");
            }
            return NoContent();
        }


       
    }
}
