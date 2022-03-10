using Microsoft.AspNetCore.Mvc;

namespace MyStore.Services.Catalog.API.Controllers
{
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        public ItemsController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{itemId:guid}")]
        public IActionResult Get(Guid itemId)
        {
            throw new NotImplementedException();
        }
    }
}
