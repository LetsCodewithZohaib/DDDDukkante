using AutoMapper;
using Dukkantek.Inventory.Api.Base;
using Dukkantek.Inventory.Application.Categories.Commands;
using Dukkantek.Inventory.Application.Categories.Models.Result;
using Dukkantek.Inventory.Application.Categories.Queries;
using Dukkantek.Inventory.Application.Common.Models.Results;
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
    /// this is the categories endpoints
    /// </summary>
    [Route("[controller]")]
    public class CategoriesController : BaseController
    {
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesController(IMediator mediator, IMapper mapper, ILogger<CategoriesController> logger) : base(mediator, mapper)
        {
            _logger = logger;
        }

        /// <summary>
        /// gets all categories in the system
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(typeof(IEnumerable<GetCategoryQueryResult>), StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _mediator.Send(new GetCategoriesQuery());
            return Ok(categories);
        }

        /// <summary>
        ///  gets a single category by ID
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("{categoryId}", Name = nameof(GetCategory)), ProducesResponseType(typeof(GetCategoryQueryResult), StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> GetCategory(Guid categoryId)
        {
            var category = await _mediator.Send(new GetCategoryQuery { Id = categoryId });
            return Ok(category);
        }


        /// <summary>
        /// creates a new category
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost(Name = nameof(CreateCategory)), ProducesResponseType(typeof(GetCategoryQueryResult), StatusCodes.Status201Created), ProducesDefaultResponseType]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtRoute(nameof(GetCategory), new { categoryid = result.Id }, result);
        }

        /// <summary>
        /// updates an category
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut(Name = nameof(UpdateCategory)), ProducesResponseType(typeof(GetCategoryQueryResult), StatusCodes.Status201Created), ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return AcceptedAtRoute(nameof(GetCategory), new { categoryid = result.Id }, result);
        }

        /// <summary>
        /// deletes single category
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete(Name = nameof(DeleteCategory)), ProducesResponseType(typeof(DeleteCommandResult), StatusCodes.Status201Created), ProducesResponseType(typeof(string), StatusCodes.Status404NotFound), ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryCommand command)
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
