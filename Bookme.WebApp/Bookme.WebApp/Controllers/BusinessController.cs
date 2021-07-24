using Bookme.Data.Models;
using Bookme.Services.Contracts;
using Bookme.ViewModels.Business;
using Bookme.WebApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bookme.WebApp.Controllers
{
    public class BusinessController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBusinessService businessService;

        public BusinessController(UserManager<ApplicationUser> userManager, IBusinessService businessService)
        {
            this.userManager = userManager;
            this.businessService = businessService;
        }

        [Authorize]
        public IActionResult Create()
        {
            if (this.User.IsInRole("Business"))
            {
                return BadRequest(error: "You already have registered business.");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateBusinessViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (this.User.IsInRole("Business"))
            {
                return BadRequest(error: "You already have registered business.");
            }

            var userId = this.User.GetId();

            var user = await userManager.FindByIdAsync(userId);
            await userManager.AddToRoleAsync(user, "Business");

            businessService.CreateBusiness(model, userId);

            return Redirect("/Business/BookingConfiguration");
        }
    }
}
