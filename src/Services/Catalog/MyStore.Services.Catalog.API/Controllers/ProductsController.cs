using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyStore.Services.Catalog.Application.Commands.Queries;
using MyStore.Services.Catalog.Application.Responses.Products;

namespace MyStore.Services.Catalog.API.Controllers
{
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProductResponse[]), 200)]
        public async Task<IActionResult> GetProducts([FromQuery]GetProductsQuery query)
        {
            var response = await _mediator.Send(query);

            return new ObjectResult(response);
        }

        [HttpPost]
        public IActionResult CreateProduct()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{product:guid}")]
        public IActionResult GetProduct(Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}
