using Bookme.ViewModels.OfferedServices;
using Bookme.WebApp.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace Bookme.Test.Routes
{
    public class OfferedServiceControllerTest
    {
        [Fact]
        public void GetAllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/OfferedServices/All")
                .To<OfferedServicesController>(c => c.All());

        [Fact]
        public void GetAddRouteShouldBeMapped()
             => MyRouting
                 .Configuration()
                 .ShouldMap("/OfferedServices/Add")
                 .To<OfferedServicesController>(c => c.Add());

        [Fact]
        public void PostAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/OfferedServices/Add")
                    .WithMethod(HttpMethod.Post))
                .To<OfferedServicesController>(c => c.Add(With.Any<AddOfferedServiceViewModel>()));

        [Fact]
        public void GetDetailsRouteShouldBeMapped()
             => MyRouting
                 .Configuration()
                 .ShouldMap("/OfferedServices/Details")
                 .To<OfferedServicesController>(c => c.Details(With.Any<int>()));

        [Fact]
        public void GetEditRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/OfferedServices/Edit")
                .To<OfferedServicesController>(c => c.Edit(With.Any<int>()));

        [Fact]
        public void PostEditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/OfferedServices/Edit")
                    .WithMethod(HttpMethod.Post))
                .To<OfferedServicesController>(c => c.Edit(With.Any<int>()));

        [Fact]
        public void GetDeleteRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/OfferedServices/Delete")
                .To<OfferedServicesController>(c => c.Delete(With.Any<int>()));
    }
}
