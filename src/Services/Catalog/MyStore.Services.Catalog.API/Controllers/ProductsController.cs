using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyStore.Services.Catalog.Application.Commands;
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
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery query)
        {
            var response = await _mediator.Send(query);

            return new ObjectResult(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductDetailResponse), 201)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var response = await _mediator.Send(command);

            return new ObjectResult(response);
        }

        [HttpGet("{productId:guid}")]
        [ProducesResponseType(typeof(ProductDetailResponse), 200)]
        public async Task<IActionResult> GetProduct(Guid productId)
        {
            var response = await _mediator.Send(new GetProductQuery { ProductId = productId });

            return new ObjectResult(response);
        }
    }
}
