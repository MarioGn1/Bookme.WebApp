using Bookme.Services.Contracts;
using Bookme.ViewModels.Categories;
using Bookme.WebApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookme.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IBusinessOffersService offersService;

        public CategoryController(ICategoryService service, IBusinessOffersService offersService)
        {
            this.categoryService = service;
            this.offersService = offersService;
        }

        [Authorize]
        public IActionResult All([FromQuery] CategoryAllMembersViewModel query, int Id)
        {
            var model = categoryService.GetCategoryMembers(
                Id, 
                query.BusinessName, 
                query.ServiceName, 
                query.SortCriteria, 
                query.CurrentPage, 
                CategoryAllMembersViewModel.BusinessesPerPage);

            query.TotalMembers = model.TotalMembers;
            query.CurrentPage = model.CurrentPage;
            query.Members = model.Members;

            return View(query);
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var userId = this.User.GetId();

            if (userId == id)
            {
                return Redirect("/OfferedServices/All");
            }
            var services = offersService.GetBusinesDetails(id);
            return View(services);
        }
    }
}
