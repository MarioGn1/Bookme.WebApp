using Bookme.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookme.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IBusinessOffersService offersService;

        public CategoryController(ICategoryService service, IBusinessOffersService offersService)
        {
            this.categoryService = service;
            this.offersService = offersService;
        }

        [Authorize]
        public IActionResult All(int id)
        {
            var models = categoryService.GetCategoryMembers(id);

            return View(models);
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var services = offersService.GetAllBusinessServices(id);
            return View(services);
        }
    }
}
