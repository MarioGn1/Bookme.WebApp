using Bookme.ViewModels.BookingConfiguration;
using Bookme.WebApp.Controllers;
using Bookme.WebApp.Controllers.Constants;
using MyTested.AspNetCore.Mvc;
using Xunit;

using static Bookme.Test.Data.User;

namespace Bookme.Test.Controllers
{
    public class BookingConfigurationControllerTest
    {
        [Theory]
        [InlineData("Mario@abv.bg", RoleConstants.CLIENT)]
        public void GetShiftInfoShouldBeForAuthorizedUsersAndRedirect(string username, string role)
     => MyController<BookingConfigurationController>
         .Instance(c => c
             .WithUser(username, new[] { role })
             .WithData(OneUser, ClientIdentityRole, ClientUserRole))
         .Calling(c => c.ShiftInfo())
         .ShouldHave()
         .ActionAttributes(attributes => attributes
             .RestrictingForAuthorizedRequests())
         .AndAlso()
         .ShouldReturn()
         .Redirect();
    }
}
