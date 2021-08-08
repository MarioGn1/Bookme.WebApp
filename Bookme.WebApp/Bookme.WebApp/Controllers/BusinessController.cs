using Bookme.Data.Models;
using Bookme.Services.Contracts;
using Bookme.ViewModels.Business;
using Bookme.WebApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using static Bookme.WebApp.Controllers.Constants.RoleConstants;
using static Bookme.WebApp.Controllers.Constants.TempDataConstants;

namespace Bookme.WebApp.Controllers
{
    public class BusinessController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBusinessService businessService;
        private readonly IBusinessOffersService offersService;

        public BusinessController(UserManager<ApplicationUser> userManager, IBusinessService businessService, IBusinessOffersService offersService)
        {
            this.userManager = userManager;
            this.businessService = businessService;
            this.offersService = offersService;
        }

        [Authorize]
        public async Task<IActionResult> Info()
        {
            var isBusiness = await this.User.IsInRole(userManager, BUSINESS);
            var isClient = await this.User.IsInRole(userManager, CLIENT);

            if (!isBusiness && isClient)
            {
                return Redirect("/Business/Create");
            }

            var userId = this.User.GetId();
            var model = offersService.GetBusinesDetails(userId);

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var isBusiness = await this.User.IsInRole(userManager, BUSINESS);

            if (isBusiness)
            {
                TempData[GLOBAL_MESSAGE_WARNING_KEY] = "You already have registered business.";
                return Redirect("/");
            }

            return View();
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBusinessViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
                        
            var isBusiness = await this.User.IsInRole(userManager, BUSINESS);

            if (isBusiness)
            {
                TempData[GLOBAL_MESSAGE_WARNING_KEY] = "You already have registered business.";
                return Redirect("/");
            }            
            
            await this.User.AddToRole(userManager, BUSINESS);

            var userId = this.User.GetId();
            await businessService.CreateBusiness(model, userId);

            TempData[GLOBAL_MESSAGE_KEY] = "Congratulations, you succsessfuly create your business! Now lets do your booking configuration!";

            return Redirect("/BookingConfiguration/Configure");
        }

        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var isBusiness = await this.User.IsInRole(userManager, BUSINESS);
            var isClient = await this.User.IsInRole(userManager, CLIENT);

            if (!isBusiness && isClient)
            {
                return Redirect("/Business/Create");
            }

            var userId = this.User.GetId();
            var model = offersService.GetBusinesDetails(userId);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(CreateBusinessViewModel model)
        {
            var isBusiness = await this.User.IsInRole(userManager, BUSINESS);
            var isClient = await this.User.IsInRole(userManager, CLIENT);

            if (!isBusiness && isClient)
            {
                return Redirect("/Business/Create");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = this.User.GetId();
            await businessService.EditBusinessInfo(model, userId);

            TempData[GLOBAL_MESSAGE_KEY] = "You succsessfuly update your business information!";

            return Redirect("/Business/Info");
        }
    }
}
