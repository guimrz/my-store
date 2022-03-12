using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyStore.Services.Catalog.Application.Commands;
using MyStore.Services.Catalog.Application.Responses.Brands;

namespace MyStore.Services.Catalog.API.Controllers
{
    [Route("brands")]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public IActionResult GetBrands()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ProducesResponseType(typeof(BrandResponse), 201)]
        public async Task<IActionResult> CreateBrand([FromBody]CreateBrandCommand command)
        {
            var result = await _mediator.Send(command);

            return new ObjectResult(result);
        }
    }
}
