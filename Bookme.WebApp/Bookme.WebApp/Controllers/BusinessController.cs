using Microsoft.AspNetCore.Mvc;

namespace Bookme.WebApp.Controllers
{
    public class BusinessController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(object model)
        {
            return View();
        }
    }
}
