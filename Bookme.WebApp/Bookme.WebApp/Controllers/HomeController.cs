using Bookme.Data.Models;
using Bookme.Services.Contracts;
using Bookme.ViewModels;
using Bookme.WebApp.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using static Bookme.WebApp.Controllers.Constants.RoleConstants;

namespace Bookme.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHomeService homeService;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, IHomeService homeService)
        {
            this.userManager = userManager;
            this.homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                if (!this.User.IsInRole(CLIENT))
                {
                    var user = await userManager.FindByIdAsync(this.User.GetId());
                    await userManager.AddToRoleAsync(user, CLIENT);
                }

                var model = this.homeService.GetAllCategories();

                return View(model);
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
