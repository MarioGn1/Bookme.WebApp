using Bookme.Data.Models;
using Bookme.Services.Contracts;
using Bookme.ViewModels.BookingConfiguration;
using Bookme.WebApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public IActionResult Configure()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Configure(ConfigureBookingConfigurationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isBusiness = await this.User.IsInRole(userManager, "Business");

            if (!isBusiness)
            {
                return Redirect("/");
            }

            var userId = this.User.GetId();
            var isCreated = service.CreateBookingConfiguration(model, userId);

            if (!isCreated)
            {
                return BadRequest(error: "You already create your booking configuration. You can edit it from 'Settings' menu.");
            }            

            return Redirect("/OfferedServices/All");
        }
    }
}
