using Bookme.Data.Models;
using Bookme.ViewModels.Business;
using Bookme.WebApp.Controllers;
using Bookme.WebApp.Controllers.Constants;
using MyTested.AspNetCore.Mvc;
using System.Linq;
using Xunit;

using static Bookme.Test.Data.User;
using static Bookme.WebApp.Controllers.Constants.TempDataConstants;

namespace Bookme.Test.Controllers
{
    public class BusinessControllerTest
    {
        [Fact]
        public void GetCreateShouldBeForAuthorizedUsersAndReturnView()
           => MyController<BusinessController>
               .Instance(c => c
                  .WithData(OneUser)
                  .WithUser(OneUser.UserName, new[] { RoleConstants.CLIENT }))
               .Calling(c => c.Create())
               .ShouldHave()
               .ActionAttributes(attributes => attributes
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .View();

        [Fact]
        public void GetCreateShouldBeForAuthorizedUsersAndRedirect()
            => MyController<BusinessController>
                .Instance(c => c
                   .WithData(OneUser, BusinessIdentityRole, BusinessUserRole)
                   .WithUser(OneUser.UserName, new[] { RoleConstants.BUSINESS }))
                .Calling(c => c.Create())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Redirect();

        [Fact]
        public void PostCreateShouldReturnViewWithModelWhenInvalidModelState()
            => MyController<BusinessController>
                .Instance(c => c
                    .WithData(OneUser)
                    .WithUser(OneUser.UserName, new[] { "Client" }))
                .Calling(c => c.Create(new CreateBusinessViewModel ()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<CreateBusinessViewModel>());

        [Theory]
        [InlineData("Business", "Some coll description", "Sofia City", "New Iskar", "+359888888888", "https://github.com/MarioGn1/Bookme.WebApp")]
        public void PostCreateShouldRedirectWhenValidModelStateButAlreadyAsBusiness(
            string businessName,
            string description,
            string supportedLocation,
            string address,
            string phoneNumber,
            string imageUrl
            )
            => MyController<BusinessController>
                .Instance(c => c
                    .WithData(OneUser, BusinessUserRole, BusinessIdentityRole)
                    .WithUser(OneUser.UserName, new[] { "Business" }))
                .Calling(c => c.Create(new CreateBusinessViewModel
                {
                    BusinessName = businessName,
                    Description = description,
                    SupportedLocation = supportedLocation,
                    Address = address,
                    PhoneNumber = phoneNumber,
                    ImageUrl = imageUrl
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GLOBAL_MESSAGE_WARNING_KEY))
                .AndAlso()
                .ShouldReturn()
                .Redirect();


        [Theory]
        [InlineData("Business", "Some coll description", "Sofia City", "New Iskar", "+359888888888", "https://github.com/MarioGn1/Bookme.WebApp")]
        public void PostCreateShouldBeForAuthorizedUsersAndRedirectWithValidModel(
            string businessName,
            string description,
            string supportedLocation,
            string address,
            string phoneNumber,
            string imageUrl
            )
            => MyController<BusinessController>
                .Instance(c => c
                    .WithData(OneUser, BusinessIdentityRole)
                    .WithUser(OneUser.UserName, new[] { "Client" }))
                .Calling(c => c.Create(new CreateBusinessViewModel
                {
                    BusinessName = businessName,
                    Description = description,
                    SupportedLocation = supportedLocation,
                    Address = address,
                    PhoneNumber = phoneNumber,
                    ImageUrl = imageUrl
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Business>(businesses => businesses
                        .Any(b => b.UserId == OneUser.Id)))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GLOBAL_MESSAGE_KEY))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<BookingConfigurationController>(c => c.Configure()));
    }
}
