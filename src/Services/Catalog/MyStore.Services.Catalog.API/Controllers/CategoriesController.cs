using Microsoft.AspNetCore.Mvc;

namespace MyStore.Services.Catalog.API.Controllers
{
    [Route("categories")]
    public class CategoriesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCategories()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult CreateCategory()
        {
            throw new NotImplementedException();
        }
    }
}
