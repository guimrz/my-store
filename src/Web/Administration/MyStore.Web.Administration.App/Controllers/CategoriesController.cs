using Microsoft.AspNetCore.Mvc;
using MyStore.Web.Administration.App.Models.Categories;
using MyStore.Web.Administration.App.Services;

namespace MyStore.Web.Administration.App.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CatalogService _catalogService;
		public CategoriesController(CatalogService catalogService)
		{
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
		}
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List(int count = 20, int offset = 0)
		{
            return View(await _catalogService.GetCategoriesAsync(count, offset));
		}

        public IActionResult Create()
		{
            return View(new CreateCategoryViewModel());
		}

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel createCategoryViewModel)
		{
            await _catalogService.CreateCategoryAsync(createCategoryViewModel);

            return RedirectToAction(nameof(List));
		}
    }
}
