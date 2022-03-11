using Microsoft.AspNetCore.Mvc;

namespace MyStore.Services.Catalog.API.Controllers
{
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            throw new NotImplementedException();
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
