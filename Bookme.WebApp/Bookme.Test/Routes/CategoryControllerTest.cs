using Bookme.ViewModels.Categories;
using Bookme.WebApp.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace Bookme.Test.Routes
{
    public class CategoryControllerTest
    {
        [Fact]
        public void GetAllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Category/All")
                .To<CategoryController>(c => c.All(With.Any<CategoryAllMembersViewModel>(), With.Any<int>()));

        [Fact]
        public void GetDetailsRouteShouldBeMapped()
             => MyRouting
                 .Configuration()
                 .ShouldMap("/Category/Details")
                 .To<CategoryController>(c => c.Details(With.Any<string>()));
    }
}
