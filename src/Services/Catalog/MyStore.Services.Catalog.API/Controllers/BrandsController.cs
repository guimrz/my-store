using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStore.Services.Catalog.Application.Commands;
using MyStore.Services.Catalog.Application.Commands.Queries;
using MyStore.Services.Catalog.Application.Responses.Brands;

namespace MyStore.Services.Catalog.API.Controllers
{
    [Authorize]
    [Route("brands")]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(BrandResponse[]), 200)]
        public async Task<IActionResult> GetBrands([FromQuery]GetBrandsQuery query)
        {
            var result = await _mediator.Send(query);

            return new ObjectResult(result);
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
