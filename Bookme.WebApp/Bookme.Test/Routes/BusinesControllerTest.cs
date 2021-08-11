using Bookme.ViewModels.Business;
using Bookme.WebApp.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace Bookme.Test.Routes
{
    public class BusinesControllerTest
    {
        [Fact]
        public void GetInfoRouteShouldBeMapped()
             => MyRouting
                 .Configuration()
                 .ShouldMap("/Business/Info")
                 .To<BusinessController>(c => c.Info());

        [Fact]
        public void GetCreateRouteShouldBeMapped()
             => MyRouting
                 .Configuration()
                 .ShouldMap("/Business/Create")
                 .To<BusinessController>(c => c.Create());

        [Fact]
        public void PostCreateShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Business/Create")
                    .WithMethod(HttpMethod.Post))
                .To<BusinessController>(c => c.Create(With.Any<CreateBusinessViewModel>()));

        [Fact]
        public void GetEditRouteShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Business/Edit")
              .To<BusinessController>(c => c.Edit());

        [Fact]
        public void PostEditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Business/edit")
                    .WithMethod(HttpMethod.Post))
                .To<BusinessController>(c => c.Edit(With.Any<CreateBusinessViewModel>()));
    }
}
