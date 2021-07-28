using Bookme.Services.Contracts;
using Bookme.ViewModels.OfferedServices;
using Bookme.WebApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Bookme.WebApp.Controllers.Constants.RoleConstants;

namespace Bookme.WebApp.Controllers
{
    public class OfferedServicesController : Controller
    {
        private readonly IBusinessOffersService offersService;

        public OfferedServicesController(IBusinessOffersService offersService)
        {
            this.offersService = offersService;
        }

        [Authorize]
        public IActionResult All()
        {
            if (IsAuthorized())
            {
                return Unauthorized();
            }

            var currentUserServices = offersService.GetAllBusinessServices(this.User.GetId());

            return View(currentUserServices);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (IsAuthorized())
            {
                return Unauthorized();
            }

            var model = this.offersService.GetAddViewModel();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddOfferedServiceViewModel model)
        {
            if (IsAuthorized())
            {
                return Unauthorized();
            }

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

        [Authorize]
        public IActionResult Details(int id)
        {
            var serviceDetails = offersService.GetServiceDetails(id);

            return View(serviceDetails);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var userId = this.User.GetId();
            var isOwner = offersService.CheckOwnership(id, userId);

            if (!isOwner && IsAuthorized())
            {
                return Unauthorized();
            }

            offersService.DeleteService(id);

            return Redirect("/OfferedServices/All");
        }

        private bool IsAuthorized()
        {
            return !this.User.IsInRole(BUSINESS) && !this.User.IsInRole(ADMIN);
        }
    }
}
