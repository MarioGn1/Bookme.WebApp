using Bookme.Services.Contracts;
using Bookme.WebApp.Infrastructure;
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
            if (this.User.GetId() == id)
            {
                return Redirect("/OfferedServices/All");
            }
            var services = offersService.GetBusinesDetails(id);
            return View(services);
        }
    }
}
