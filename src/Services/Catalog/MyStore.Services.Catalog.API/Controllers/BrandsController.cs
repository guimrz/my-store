using Microsoft.AspNetCore.Mvc;

namespace MyStore.Services.Catalog.API.Controllers
{
    [Route("brands")]
    public class BrandsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBrands()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult CreateBrand()
        {
            throw new NotImplementedException();
        }

    }
}
