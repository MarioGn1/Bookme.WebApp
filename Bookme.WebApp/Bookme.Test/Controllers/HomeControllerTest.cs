using Xunit;
using MyTested.AspNetCore.Mvc;
using Bookme.WebApp.Controllers;
using Bookme.ViewModels.HomeModels;
using System.Collections.Generic;

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
        public void GetIndexShouldReturnViewWithUser()
            => MyController<HomeController>
                .Instance()
                .WithUser(TestUser.Identifier, "Mario", "Client")
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
