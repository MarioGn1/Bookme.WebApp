using Bookme.ViewModels.Booking;
using Bookme.WebApp.Areas.Booking.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;
using System;
using System.Linq;

using static Bookme.Test.Data.User;
using static Bookme.Test.Data.DataInstances;
using static Bookme.WebApp.Controllers.Constants.RoleConstants;
using static Bookme.WebApp.Controllers.Constants.TempDataConstants;
using Bookme.WebApp.Controllers;

namespace Bookme.Test.Areas.Booking.Controllers
{
    public class BookingControllerTest
    {
        [Fact]
        public void GetInfoShouldBeForAuthorizedUsersAndReturnUnautorized()
            => MyController<BookingController>
                .Instance(c => c
                   .WithData(OneUser, OneOfferedService)
                   .WithUser(OneUser.UserName, new[] { CLIENT }))
                .Calling(c => c.Index(OneOfferedService.Id))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void GetInfoShouldBeForAuthorizedUsersAndReturnViewWithModel()
            => MyController<BookingController>
                .Instance(c => c
                   .WithData(SecondUser, OneUser, OneOfferedService)
                   .WithUser(SecondUser.Id, SecondUser.UserName, new[] { CLIENT }))
                .Calling(c => c.Index(OneOfferedService.Id))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ServiceBookingViewModel>());

        [Fact]
        public void PostInfoShouldBeForAuthorizedUsersAndReturnUnautorized()
            => MyController<BookingController>
                .Instance(c => c
                   .WithData(OneUser, OneOfferedService)
                   .WithUser(OneUser.UserName, new[] { CLIENT }))
                .Calling(c => c.Index(new BookServiceViewModel { OwnerId = OneUser.Id, ClientId = OneUser.Id }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Unauthorized();

        [Theory]
        [InlineData(
            1, 
            60,
            "TestService",
            "TestId",
            "TestId2",
            "FirstName",
            "LastName",
            "+359 885 123123",
            "2021-06-07 10:30:00",
            "Some testing text here")]
        public void PostCreateShouldBeForAuthorizedUsersAndRedirectWithValidModel(
            int serviceId,
            int duraion,
            string Name,
            string ownerId,
            string clientId,
            string clientFirtName,
            string clientLastName,
            string clientPhone,
            string date,
            string notes)
            => MyController<BookingController>
                .Instance(c => c
                    .WithData(OneUser, SecondUser, OneOfferedService, OneBusiness)
                    .WithUser(SecondUser.Id, SecondUser.UserName, new[] { CLIENT }))
                .Calling(c => c.Index(new BookServiceViewModel
                {
                    ServiceId = serviceId,
                    Duration = duraion,
                    Name = Name,
                    OwnerId = ownerId,
                    ClientId = clientId,
                    FirstName = clientFirtName,
                    LastName = clientLastName,
                    PhoneNumber = clientPhone,
                    Date = date,
                    Notes = notes
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .Data(data => data
                    .WithSet<Bookme.Data.Models.Booking>(businesses => businesses
                        .Any(b => b.BusinessId == ownerId
                        && b.ClientId == clientId
                        && b.ClientFirstName == clientFirtName
                        && b.ClientLastName == clientLastName
                        && b.ClientPhoneNumber == clientPhone
                        && b.BookedService == Name
                        && b.Duration == duraion
                        && b.Notes == notes)))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GLOBAL_MESSAGE_KEY))
                .AndAlso()
                .ShouldReturn()
                .Redirect();

        [Theory]
        [InlineData(
            1,
            60,
            "TestService",
            "TestId",
            "TestId2",
            "FirstName",
            "LastName",
            "+359 885 123123",
            "2021-06-07 10:30:00",
            "Some testing text here")]
        public void PostCreateShouldBeForAuthorizedUsersAndRedirectWithInvalidDate(
            int serviceId,
            int duraion,
            string Name,
            string ownerId,
            string clientId,
            string clientFirtName,
            string clientLastName,
            string clientPhone,
            string date,
            string notes)
            => MyController<BookingController>
                .Instance(c => c
                    .WithData(OneUser, SecondUser, OneOfferedService, OneBooking)
                    .WithUser(SecondUser.Id, SecondUser.UserName, new[] { CLIENT }))
                .Calling(c => c.Index(new BookServiceViewModel
                {
                    ServiceId = serviceId,
                    Duration = duraion,
                    Name = Name,
                    OwnerId = ownerId,
                    ClientId = clientId,
                    FirstName = clientFirtName,
                    LastName = clientLastName,
                    PhoneNumber = clientPhone,
                    Date = date,
                    Notes = notes
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GLOBAL_MESSAGE_WARNING_KEY))
                .AndAlso()
                .ShouldReturn()
                .Redirect();
    }
}
