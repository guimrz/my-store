using Microsoft.AspNetCore.Mvc;

namespace MyStore.Web.Administration.App.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
