using Bookme.WebApp.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

using static Bookme.Test.Data.User;

namespace Bookme.Test.Controllers
{
    public class BusinessControllerTest
    {
        [Fact]
        public void GetCreateShouldBeForAuthorizedUsersAndReturnView()
           => MyController<BusinessController>
               .Instance(c => c
                  .WithData(OneUser)
                  .WithUser(OneUser.Id, OneUser.UserName, "Business"))               
               .Calling(c => c.Create())
               .ShouldHave()
               .ActionAttributes(attributes => attributes
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .View();
    }
}
