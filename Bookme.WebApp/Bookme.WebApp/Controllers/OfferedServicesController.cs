using Bookme.Services;
using Bookme.Services.Contracts;
using Bookme.ViewModels.OfferedServices;
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
            var model = this.offersService.GetAddViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AddOfferedServiceViewModel model)
        {
            var categoryId = model.OfferedService.ServiceCategoryId;
            if (!this.offersService.CheckForCategory(categoryId))
            {
                this.ModelState.AddModelError(nameof(categoryId), "Category does not exist.");
            }

            var visitationTypeId = model.OfferedService.ServiceVisitationId;
            if (!this.offersService.CheckForVisitationType(visitationTypeId))
            {
                this.ModelState.AddModelError(nameof(visitationTypeId), "Visitation type does not exist.");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = this.offersService.GetAllCategories();
                model.VisitationTypes = this.offersService.GetAllVisitationTypes();

                return View(model);
            }

            return Redirect("/OfferedServices/All");
        }
    }
}
