using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyStore.Services.Catalog.Application.Commands;
using MyStore.Services.Catalog.Application.Commands.Queries;
using MyStore.Services.Catalog.Application.Responses.Categories;

namespace MyStore.Services.Catalog.API.Controllers
{
    [Route("categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(CategoryResponse[]), 200)]
        public async Task<IActionResult> GetCategories([FromQuery]GetCategoriesQuery query)
        {
            var response = await _mediator.Send(query);

            return new ObjectResult(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CategoryResponse), 201)]
        public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryCommand command)
        {
            var response = await _mediator.Send(command);

            return new ObjectResult(response);
        }
    }
}
