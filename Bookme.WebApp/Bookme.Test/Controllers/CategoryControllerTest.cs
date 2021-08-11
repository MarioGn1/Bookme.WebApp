using Bookme.Data.Models;
using Bookme.ViewModels.Business;
using Bookme.ViewModels.Categories;
using Bookme.WebApp.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

using static Bookme.Test.Data.Category;

namespace Bookme.Test.Controllers
{
    public class CategoryControllerTest
    {
        [Fact]
        public void GetAllShouldBeForAuthorizedUsersAndReturnView()
            => MyController<CategoryController>
                .Instance()
                .WithUser()
                .Calling(c => c.All(With.Any<CategoryAllMembersViewModel>(), With.Value(6)))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<CategoryAllMembersViewModel>());

        [Fact]
        public void GetDetailsShouldBeForAuthorizedUsersAndReturnView()
            => MyController<CategoryController>
                .Instance()
                .WithUser()
                .Calling(c => c.Details(With.Value(TestUser.Identifier)))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Redirect();
    }
}
