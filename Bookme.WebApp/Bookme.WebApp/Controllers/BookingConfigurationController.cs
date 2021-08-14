using Bookme.Data.Models;
using Bookme.Services.Contracts;
using Bookme.ViewModels.BookingConfiguration;
using Bookme.WebApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using static Bookme.WebApp.Controllers.Constants.RoleConstants;
using static Bookme.WebApp.Controllers.Constants.TempDataConstants;

namespace Bookme.WebApp.Controllers
{
    public class BookingConfigurationController : Controller
    {
        private readonly IBookingConfigurationService service;
        private readonly UserManager<ApplicationUser> userManager;

        public BookingConfigurationController(IBookingConfigurationService service, UserManager<ApplicationUser> userManager)
        {
            this.service = service;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> ShiftInfo()
        {
            var isBusiness = await this.User.IsInRole(userManager, BUSINESS);
            var isClient = await this.User.IsInRole(userManager, CLIENT);

            if (!isBusiness && isClient)
            {
                return Redirect("/Business/Create");
            }

            var userId = this.User.GetId();
            var model = service.GetBookingConfigurationInfo(userId);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ShiftInfo(ConfigureBookingConfigurationViewModel model)
        {
            var isBusiness = await this.User.IsInRole(userManager, BUSINESS);
            var isClient = await this.User.IsInRole(userManager, CLIENT);

            if (!isBusiness && isClient)
            {
                return Redirect("/Business/Create");
            }

            CheckModel(model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = this.User.GetId();
            var isEdited = service.EditBookingConfiguration(model, userId);

            if (!isEdited)
            {
                TempData[GLOBAL_MESSAGE_DANGER_KEY] = "You have no permition for this action!";
                return Redirect("/");
            }

            TempData[GLOBAL_MESSAGE_KEY] = "You successfuly update your booking configuration!";

            return Redirect("/BookingConfiguration/ShiftInfo");
        }

        [Authorize]
        public async Task<IActionResult> Configure()
        {
            var isBusiness = await this.User.IsInRole(userManager, BUSINESS);
            var isClient = await this.User.IsInRole(userManager, CLIENT);

            if (!isBusiness && isClient)
            {
                return Redirect("/Business/Create");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Configure(ConfigureBookingConfigurationViewModel model)
        {
            var isBusiness = await this.User.IsInRole(userManager, BUSINESS);
            var isClient = await this.User.IsInRole(userManager, CLIENT);

            if (!isBusiness && isClient)
            {
                return Redirect("/Business/Create");
            }

            CheckModel(model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = this.User.GetId();
            var isCreated = await service.CreateBookingConfiguration(model, userId);

            if (!isCreated)
            {
                TempData[GLOBAL_MESSAGE_WARNING_KEY] = "You already create your booking configuration. You can edit it from this page!";
                return Redirect("/BookingConfiguration/ShiftInfo");
            }

            TempData[GLOBAL_MESSAGE_KEY] = "You successfuly create your booking configuration!";

            return Redirect("/OfferedServices/All");
        }

        private void CheckModel(ConfigureBookingConfigurationViewModel model)
        {
            if (model.ServiceInterval == 0)
            {
                this.ModelState.AddModelError(nameof(model.ServiceInterval), "Invalid Service Interval.");
            }

            if (model.ShiftStart == model.ShiftEnd)
            {
                this.ModelState.AddModelError($"{nameof(model.ShiftStart)} {nameof(model.ShiftEnd)}", "Invalid Work Interval.");
            }
        }
    }
}
