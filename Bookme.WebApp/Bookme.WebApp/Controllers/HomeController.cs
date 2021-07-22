using Bookme.Data.Models;
using Bookme.Services.Contracts;
using Bookme.ViewModels;
using Bookme.WebApp.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Bookme.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IHomeService homeService;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, IHomeService homeService)
        {
            _logger = logger;
            this.userManager = userManager;
            this.homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                if (!this.User.IsInRole("Client"))
                {
                    var user = await userManager.FindByIdAsync(this.User.GetId());
                    await userManager.AddToRoleAsync(user, "Client");
                }
                if (this.User.IsInRole("Business"))
                {
                    return Redirect("/OfferedServices/All");
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
