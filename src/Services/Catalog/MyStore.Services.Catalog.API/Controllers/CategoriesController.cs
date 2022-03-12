using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyStore.Services.Catalog.Application.Commands;
using MyStore.Services.Catalog.Application.Responses;

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
        public IActionResult GetCategories()
        {
            throw new NotImplementedException();
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
