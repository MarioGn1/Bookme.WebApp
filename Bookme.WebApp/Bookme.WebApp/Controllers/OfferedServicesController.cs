using Bookme.Data.Models;
using Bookme.Services.Contracts;
using Bookme.ViewModels.OfferedServices;
using Bookme.WebApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Bookme.WebApp.Controllers.Constants.RoleConstants;
using static Bookme.WebApp.Controllers.Constants.TempDataConstants;

namespace Bookme.WebApp.Controllers
{
    public class OfferedServicesController : Controller
    {
        private readonly IBusinessOffersService offersService;
        private readonly UserManager<ApplicationUser> userManager;

        public OfferedServicesController(IBusinessOffersService offersService, UserManager<ApplicationUser> userManager)
        {
            this.offersService = offersService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            if (await IsNotAuthorized())
            {
                return Unauthorized();
            }

            var currentUserServices = offersService.GetAllBusinessServices(this.User.GetId());

            return View(currentUserServices);
        }

        [Authorize]
        public async Task<IActionResult> Add()
        {
            if (await IsNotAuthorized())
            {
                return Unauthorized();
            }

            var model = this.offersService.GetAddViewModel();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddOfferedServiceViewModel model)
        {
            if (await IsNotAuthorized())
            {
                return Unauthorized();
            }

            ValidateCategory(model.OfferedService.ServiceCategoryId);
            ValidateVisitation(model.OfferedService.ServiceVisitationId);

            if (!ModelState.IsValid)
            {
                model.Categories = this.offersService.GetAllCategories();
                model.VisitationTypes = this.offersService.GetAllVisitationTypes();

                return View(model);
            }

            var isDurationValid = this.offersService.CreateOfferedService(model, this.User.GetId());

            if (!isDurationValid)
            {
                model.Categories = this.offersService.GetAllCategories();
                model.VisitationTypes = this.offersService.GetAllVisitationTypes();

                TempData[GLOBAL_MESSAGE_WARNING_KEY] = $"Your business service interval is lower than the choosen service duration! Please chose lower duration of your service or change your 'Booking configuration -> Service interval' from settings menu.";

                return View(model);
            }

            TempData[GLOBAL_MESSAGE_KEY] = $"You succsessfuly create {model.OfferedService.Name} service!";

            return Redirect("/OfferedServices/All");
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var serviceDetails = offersService.GetServiceDetails(id);

            return View(serviceDetails);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = this.User.GetId();
            var isOwner = offersService.CheckOwnership(id, userId);

            if (await IsNotAuthorized())
            {
                return Unauthorized();
            }

            if (!isOwner && await IsNotAdmin())
            {
                return Unauthorized();
            }

            var model = offersService.GetEditViewModel(id);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(AddOfferedServiceViewModel model, int id)
        {
            var userId = this.User.GetId();
            var isOwner = offersService.CheckOwnership(id, userId);

            if (await IsNotAuthorized())
            {
                return Unauthorized();
            }

            if (!isOwner && await IsNotAdmin())
            {
                return Unauthorized();
            }

            ValidateCategory(model.OfferedService.ServiceCategoryId);
            ValidateVisitation(model.OfferedService.ServiceVisitationId);

            if (!ModelState.IsValid)
            {
                model.Categories = this.offersService.GetAllCategories();
                model.VisitationTypes = this.offersService.GetAllVisitationTypes();

                return View(model);
            }

            this.offersService.EditOfferedService(model, id);

            TempData[GLOBAL_MESSAGE_KEY] = $"You succsessfuly update {model.OfferedService.Name} service!";

            return Redirect($"/OfferedServices/Details/{id}");
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.User.GetId();
            var isOwner = offersService.CheckOwnership(id, userId);

            if (await IsNotAuthorized())
            {
                return Unauthorized();
            }

            if (!isOwner && await IsNotAdmin())
            {
                return Unauthorized();
            }

            offersService.DeleteService(id);

            TempData[GLOBAL_MESSAGE_WARNING_KEY] = "The service is deleted!";

            return Redirect("/OfferedServices/All");
        }

        private async Task<bool> IsNotAuthorized()
        {
            var isBusiness = await this.User.IsInRole(userManager, BUSINESS);           

            return !isBusiness && await IsNotAdmin();
        }

        private async Task<bool> IsNotAdmin()
        {
            var isAdmin = await this.User.IsInRole(userManager, ADMIN);
            return !isAdmin;
        }

        private void ValidateVisitation(int visitationTypeId)
        {
            if (!this.offersService.CheckForVisitationType(visitationTypeId))
            {
                this.ModelState.AddModelError(nameof(visitationTypeId), "Visitation type does not exist.");
            }
        }

        private void ValidateCategory(int categoryId)
        {
            if (!this.offersService.CheckForCategory(categoryId))
            {
                this.ModelState.AddModelError(nameof(categoryId), "Category does not exist.");
            }
        }
    }
}
