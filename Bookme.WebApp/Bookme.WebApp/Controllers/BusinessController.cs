using Bookme.Data.Models;
using Bookme.Services.Contracts;
using Bookme.ViewModels.Business;
using Bookme.WebApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Bookme.WebApp.Controllers.Constants.RoleConstants;

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
        public async Task<IActionResult> Create()
        {
            var isBusiness = await this.User.IsInRole(userManager, BUSINESS);

            if (isBusiness)
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
                        
            var isBusiness = await this.User.IsInRole(userManager, BUSINESS);

            if (isBusiness)
            {
                return BadRequest(error: "You already have registered business.");
            }            
            
            await this.User.AddToRole(userManager, BUSINESS);

            var userId = this.User.GetId();
            await businessService.CreateBusiness(model, userId);            

            return Redirect("/BookingConfiguration/Configure");
        }
    }
}
