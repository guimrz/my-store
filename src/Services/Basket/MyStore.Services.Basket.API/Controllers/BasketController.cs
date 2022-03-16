using Microsoft.AspNetCore.Mvc;

namespace MyStore.Services.Basket.API.Controllers
{
    [Route("api/basket")]
    public class BasketController
    {
        public BasketController()
        {

        }

        [HttpGet]
        public IActionResult GetBasket()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult UpdateBasket(object basket)
        {
            throw new NotImplementedException();
        }
    }
}
