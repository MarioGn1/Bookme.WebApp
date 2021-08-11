using Bookme.ViewModels.BookingConfiguration;
using Bookme.WebApp.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace Bookme.Test.Routes
{
    public class BookingConfigurationControllerTest
    {
        [Fact]
        public void GetShiftInfoRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/BookingConfiguration/ShiftInfo")
                .To<BookingConfigurationController>(c => c.ShiftInfo());

        [Fact]
        public void PostShiftInfoShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/BookingConfiguration/ShiftInfo")
                    .WithMethod(HttpMethod.Post))
                .To<BookingConfigurationController>(c => c.ShiftInfo(With.Any<ConfigureBookingConfigurationViewModel>()));

        [Fact]
        public void GetConfigureRouteShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap("/BookingConfiguration/Configure")
               .To<BookingConfigurationController>(c => c.Configure());

        [Fact]
        public void PostConfigureShiftInfoShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/BookingConfiguration/Configure")
                    .WithMethod(HttpMethod.Post))
                .To<BookingConfigurationController>(c => c.Configure(With.Any<ConfigureBookingConfigurationViewModel>()));
    }
}
