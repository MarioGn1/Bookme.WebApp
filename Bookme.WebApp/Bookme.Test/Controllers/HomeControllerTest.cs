using Bookme.WebApp.Controllers;
using Bookme.ViewModels.HomeModels;
using MyTested.AspNetCore.Mvc;
using Xunit;
using System.Collections.Generic;

using static Bookme.Test.Data.User;
using Bookme.WebApp.Controllers.Constants;

namespace Bookme.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void GetIndexShouldReturnViewWithoutUser()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();

        [Fact]
        public void GetIndexShouldReturnViewWithUserWithoutRole()
            => MyController<HomeController>
                .Instance(c => c
                    .WithData(OneUser, ClientIdentityRole)
                    .WithUser())
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<IEnumerable<ClientHomeViewModel>>());

        [Theory]
        [InlineData("Mario@abv.bg", RoleConstants.CLIENT)]
        public void GetIndexShouldReturnViewWithUser(string username, string role)
            => MyController<HomeController>
                .Instance(c => c
                    .WithData(OneUser, ClientIdentityRole, ClientUserRole)
                    .WithUser(username, new[] { role }))
                .Calling(c => c.Index())            
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<IEnumerable<ClientHomeViewModel>>());

        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();
    }
}
