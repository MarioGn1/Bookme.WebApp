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
                .Instance(c => c
                    .WithData(OneOfferedService,OneBusiness)
                    .WithUser())                
                .Calling(c => c.All(new CategoryAllMembersViewModel(), With.Value(6)))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<CategoryAllMembersViewModel>());

        [Fact]
        public void GetDetailsShouldBeForAuthorizedUsersAndRedirect()
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

        [Fact]
        public void GetDetailsShouldBeForAuthorizedUsersAndReturnView()
            => MyController<CategoryController>
                .Instance(c =>c
                    .WithData(OneBusiness, OneOfferedService)
                    .WithUser())
                .Calling(c => c.Details(With.Value("test")))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<BusinessDetailsViewModel>());
    }
}
