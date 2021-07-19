using Bookme.Services;
using Bookme.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Bookme.WebApp.Controllers
{
    public class OfferedServicesController : Controller
    {
        private readonly BusinessOffersService offersService;

        public OfferedServicesController(IBusinessOffersService offersService)
        {
            this.offersService = (BusinessOffersService)offersService;
        }

        public IActionResult All()
        {
            return View();
        }

        public IActionResult Add()
        {
            var categories = this.offersService.GetAllCategories();
            return View(categories);
        }

        [HttpPost]
        public IActionResult Add(object obj)
        {
            return Redirect("/OfferedServices/All");
        }
    }
}
