using Bookme.Services;
using Bookme.Services.Contracts;
using Bookme.ViewModels.OfferedServices;
using Bookme.WebApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult All()
        {
            var currentUserServices = offersService.GetAllBusinessServices(this.User.GetId());

            return View(currentUserServices);
        }

        [Authorize]
        public IActionResult Add()
        {
            var model = this.offersService.GetAddViewModel();

            return View(model);
        }

        [HttpPost]
        [Authorize]
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

            this.offersService.CreateOfferedService(model, this.User.GetId());

            return Redirect("/OfferedServices/All");
        }
    }
}
