using Bookme.WebApp.Areas.Booking.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

using static Bookme.Test.Data.DataInstances;

namespace Bookme.Test.Areas.Booking.Routes
{
    public class BookingControllerTest
    {
        [Fact]
        public void GetShiftInfoRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap($"/Booking/Booking/Index/{OneOfferedService.Id}")
                .To<BookingController>(c => c.Index(OneOfferedService.Id));
    }
}
