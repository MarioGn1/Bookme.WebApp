using Bookme.WebApp.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace Bookme.Test.Routes
{
    public class HomeControllerTest
    {
        [Fact]
        public void GetIndexRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index());

        [Fact]
        public void GetErrorRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
    }
}
