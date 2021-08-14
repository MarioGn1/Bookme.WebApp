using Bookme.ViewModels.Business;
using Bookme.ViewModels.Categories;
using Bookme.WebApp.Controllers;
using Bookme.WebApp.Controllers.Constants;
using MyTested.AspNetCore.Mvc;
using Xunit;

using static Bookme.Test.Data.DataInstances;
using static Bookme.Test.Data.User;

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
                    .WithData(SecondUser, OneBusiness, OneOfferedService)
                    .WithUser(SecondUser.Id, SecondUser.UserName, new[] { RoleConstants.CLIENT}))
                .Calling(c => c.Details(With.Value(TestUser.Identifier)))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<BusinessDetailsViewModel>());
    }
}
