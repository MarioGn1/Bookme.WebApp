using Bookme.Data.Models;
using Bookme.WebApp.Controllers;
using Bookme.ViewModels.OfferedServices;
using MyTested.AspNetCore.Mvc;
using Xunit;
using System.Linq;
using System.Collections.Generic;

using static Bookme.Test.Data.User;
using static Bookme.Test.Data.DataInstances;
using static Bookme.WebApp.Controllers.Constants.RoleConstants;
using static Bookme.WebApp.Controllers.Constants.TempDataConstants;

namespace Bookme.Test.Controllers
{
    public class OfferedServicesControllerTest
    {
        [Fact]
        public void GetAllShouldBeForAuthorizedUsersAndRedirect()
            => MyController<OfferedServicesController>
                .Instance(c => c
                   .WithData(OneUser, ClientIdentityRole, ClientUserRole)
                   .WithUser(OneUser.UserName, new[] { CLIENT }))
                .Calling(c => c.All())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void GetAllShouldBeForAuthorizedUsersAndReturnViewWithModel()
            => MyController<OfferedServicesController>
                .Instance(c => c
                   .WithData(OneUser, BusinessIdentityRole, BusinessUserRole, OneOfferedService)
                   .WithUser(OneUser.UserName, new[] { BUSINESS }))
                .Calling(c => c.All())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IEnumerable<GetOfferedServiceViewModel>>());

        [Fact]
        public void GetAddShouldBeForAuthorizedUsersAndReturnUnautorized()
            => MyController<OfferedServicesController>
                .Instance(c => c
                   .WithData(OneUser, ClientIdentityRole, ClientUserRole)
                   .WithUser(OneUser.UserName, new[] { CLIENT }))
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void GetAddShouldBeForAuthorizedUsersAndReturnViewWithModel()
            => MyController<OfferedServicesController>
                .Instance(c => c
                   .WithData(OneUser, BusinessIdentityRole, BusinessUserRole)
                   .WithUser(OneUser.UserName, new[] { BUSINESS }))
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AddOfferedServiceViewModel>());

        [Fact]
        public void PostAddShouldBeForAuthorizedUsersAndReturnUnauthorized()
            => MyController<OfferedServicesController>
                .Instance(c => c
                   .WithData(OneUser, ClientIdentityRole, ClientUserRole)
                   .WithUser(OneUser.UserName, new[] { CLIENT }))
                .Calling(c => c.Add(new AddOfferedServiceViewModel()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void PostAddShouldReturnViewWithModelWhenInvalidModelState()
            => MyController<OfferedServicesController>
                .Instance(c => c
                    .WithData(OneUser, BusinessIdentityRole, BusinessUserRole)
                    .WithUser(OneUser.UserName, new[] { BUSINESS }))
                .Calling(c => c.Add(new AddOfferedServiceViewModel
                {
                    OfferedService = new OfferedServiceViewModel()
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<AddOfferedServiceViewModel>());

        [Theory]
        [InlineData("Service", "Some coll description", 20.99, 10.99, 30, 1, 1, "https://github.com/MarioGn1/Bookme.WebApp")]
        public void PostAddShouldBeForAuthorizedUsersAndRedirectWithValidModel(
            string name,
            string description,
            double price,
            double visitationPrice,
            int duration,
            int serviceCategoryId,
            int serviceVisitationId,
            string imageUrl
            )
            => MyController<OfferedServicesController>
                .Instance(c => c
                    .WithData(
                        OneUser,
                        BusinessIdentityRole,
                        BusinessUserRole,
                        OneCategory,
                        OneVisitationType,
                        OneBusiness,
                        OneBookingConfiguration
                        )
                    .WithUser(OneUser.UserName, new[] { BUSINESS }))
                .Calling(c => c.Add(new AddOfferedServiceViewModel
                {
                    OfferedService = new OfferedServiceViewModel
                    {
                        Name = name,
                        Description = description,
                        Price = price,
                        VisitationPrice = visitationPrice,
                        Duration = duration,
                        ServiceCategoryId = serviceCategoryId,
                        ServiceVisitationId = serviceVisitationId,
                        ImageUrl = imageUrl
                    }
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<OfferedService>(service => service
                        .Any(s =>
                        s.UserId == OneUser.Id &&
                        s.Name == name &&
                        s.Description == description &&
                        s.Price == (decimal)price &&
                        s.VisitationPrice == (decimal)visitationPrice &&
                        s.Duration == duration &&
                        s.ServiceCategoryId == serviceCategoryId &&
                        s.ServiceVisitations.Any(v => v.VisitationTypeId == serviceVisitationId) &&
                        s.ImageUrl == imageUrl
                        )))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GLOBAL_MESSAGE_KEY))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<OfferedServicesController>(c => c.All()));

        [Theory]
        [InlineData("Service", "Some coll description", 20.99, 10.99, 50, 1, 1, "https://github.com/MarioGn1/Bookme.WebApp")]
        public void PostAddShouldBeForAuthorizedUsersAndReturnViewWhenInvalidDuration(
            string name,
            string description,
            double price,
            double visitationPrice,
            int duration,
            int serviceCategoryId,
            int serviceVisitationId,
            string imageUrl
            )
            => MyController<OfferedServicesController>
                .Instance(c => c
                    .WithData(
                        OneUser,
                        BusinessIdentityRole,
                        BusinessUserRole,
                        OneCategory,
                        OneVisitationType,
                        OneBusiness,
                        OneBookingConfiguration
                        )
                    .WithUser(OneUser.UserName, new[] { BUSINESS }))
                .Calling(c => c.Add(new AddOfferedServiceViewModel
                {
                    OfferedService = new OfferedServiceViewModel
                    {
                        Name = name,
                        Description = description,
                        Price = price,
                        VisitationPrice = visitationPrice,
                        Duration = duration,
                        ServiceCategoryId = serviceCategoryId,
                        ServiceVisitationId = serviceVisitationId,
                        ImageUrl = imageUrl
                    }
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
                .View(view => view
                    .WithModelOfType<AddOfferedServiceViewModel>());

        [Fact]
        public void GetDetailsShouldBeForAuthorizedUsersAndReturnView()
            => MyController<OfferedServicesController>
            .Instance(c => c
                    .WithData(
                        OneUser, 
                        ClientIdentityRole, 
                        ClientUserRole, 
                        OneOfferedService, 
                        OneVisitationType, 
                        OneCategory,
                        OneServiceVisitation
                        )
                    .WithUser(OneUser.UserName, new[] { CLIENT }))
            .Calling(c => c.Details(With.Value(1)))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View(view => view
                    .WithModelOfType<OfferedServiceDetailsViewModel>());

        [Fact]
        public void GetEditShouldBeForAuthorizedUsersAndReturnUnautorized()
            => MyController<OfferedServicesController>
                .Instance(c => c
                   .WithData(OneUser, ClientIdentityRole, ClientUserRole)
                   .WithUser(OneUser.UserName, new[] { CLIENT }))
                .Calling(c => c.Edit(1))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void GetEditShouldBeForAuthorizedUsersAndReturnUnautorizedWhenNotOwner()
            => MyController<OfferedServicesController>
                .Instance(c => c
                   .WithData(OneUser, BusinessIdentityRole, BusinessUserRole)
                   .WithUser(OneUser.UserName, new[] { BUSINESS }))
                .Calling(c => c.Edit(1))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void GetEditShouldBeForAuthorizedUsersAndReturnViewWithModel()
            => MyController<OfferedServicesController>
                .Instance(c => c
                   .WithData(
                    OneUser, 
                    BusinessIdentityRole, 
                    BusinessUserRole, 
                    OneOfferedService, 
                    OneCategory, 
                    OneVisitationType, 
                    OneServiceVisitation
                    )
                   .WithUser(OneUser.UserName, new[] { BUSINESS }))
                .Calling(c => c.Edit(1))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AddOfferedServiceViewModel>());


        [Fact]
        public void PostEditShouldBeForAuthorizedUsersAndReturnUnautorized()
            => MyController<OfferedServicesController>
                .Instance(c => c
                   .WithData(OneUser, ClientIdentityRole, ClientUserRole)
                   .WithUser(OneUser.UserName, new[] { CLIENT }))
                .Calling(c => c.Edit(new AddOfferedServiceViewModel
                {
                    OfferedService = new OfferedServiceViewModel()
                }, 1))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void PostEditShouldBeForAuthorizedUsersAndReturnUnautorizedWhenNotOwner()
            => MyController<OfferedServicesController>
                .Instance(c => c
                   .WithData(OneUser, BusinessIdentityRole, BusinessUserRole)
                   .WithUser(OneUser.UserName, new[] { BUSINESS }))
                .Calling(c => c.Edit(new AddOfferedServiceViewModel
                {
                    OfferedService = new OfferedServiceViewModel()
                }, 1))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void PostEditShouldBeForAuthorizedUsersAndReturnViewWithInvalidModelState()
            => MyController<OfferedServicesController>
                .Instance(c => c
                   .WithData(
                    OneUser,
                    BusinessIdentityRole,
                    BusinessUserRole,
                    OneOfferedService,
                    OneCategory,
                    OneVisitationType,
                    OneServiceVisitation
                    )
                   .WithUser(OneUser.UserName, new[] { BUSINESS }))
                .Calling(c => c.Edit(new AddOfferedServiceViewModel
                {
                    OfferedService = new OfferedServiceViewModel()
                }, 1))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AddOfferedServiceViewModel>());

        [Theory]
        [InlineData("Edited Service", "Some coll description", 20.99, 10.99, 30, 1, 1, "https://github.com/MarioGn1/Bookme.WebApp", 1)]
        public void PostEditShouldBeForAuthorizedUsersAndRedirectWithValidModel(
            string name,
            string description,
            double price,
            double visitationPrice,
            int duration,
            int serviceCategoryId,
            int serviceVisitationId,
            string imageUrl,
            int serviceId
            )
            => MyController<OfferedServicesController>
                .Instance(c => c
                    .WithData(
                        OneUser,
                        BusinessIdentityRole,
                        BusinessUserRole,
                        OneOfferedService,
                        OneCategory,
                        OneVisitationType,
                        OneServiceVisitation
                        )
                    .WithUser(OneUser.UserName, new[] { BUSINESS }))
                .Calling(c => c.Edit(new AddOfferedServiceViewModel
                {
                    OfferedService = new OfferedServiceViewModel
                    {
                        Name = name,
                        Description = description,
                        Price = price,
                        VisitationPrice = visitationPrice,
                        Duration = duration,
                        ServiceCategoryId = serviceCategoryId,
                        ServiceVisitationId = serviceVisitationId,
                        ImageUrl = imageUrl
                    }
                }, serviceId))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<OfferedService>(service => service
                        .Any(s =>
                        s.UserId == OneUser.Id &&
                        s.Name == name &&
                        s.Description == description &&
                        s.Price == (decimal)price &&
                        s.VisitationPrice == (decimal)visitationPrice &&
                        s.Duration == duration &&
                        s.ServiceCategoryId == serviceCategoryId &&
                        s.ServiceVisitations.Any(v => v.VisitationTypeId == serviceVisitationId) &&
                        s.ImageUrl == imageUrl
                        )))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GLOBAL_MESSAGE_KEY))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<OfferedServicesController>(c => c.Details(serviceId)));

        [Fact]
        public void GetDeleteShouldBeForAuthorizedUsersAndReturnUnautorized()
            => MyController<OfferedServicesController>
                .Instance(c => c
                   .WithData(OneUser, ClientIdentityRole, ClientUserRole)
                   .WithUser(OneUser.UserName, new[] { CLIENT }))
                .Calling(c => c.Delete(1))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void GetDeleteShouldBeForAuthorizedUsersAndReturnUnautorizedWhenNotOwner()
            => MyController<OfferedServicesController>
                .Instance(c => c
                   .WithData(
                        OneUser,
                        BusinessIdentityRole,
                        BusinessUserRole
                        )
                   .WithUser(OneUser.UserName, new[] { BUSINESS }))
                .Calling(c => c.Delete(1))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void GetDeleteShouldBeForAuthorizedUsersAndRedirectAfterSuccess()
            => MyController<OfferedServicesController>
                .Instance(c => c
                   .WithData(
                        OneUser,
                        BusinessIdentityRole,
                        BusinessUserRole,
                        OneOfferedService
                        )
                   .WithUser(OneUser.UserName, new[] { BUSINESS }))
                .Calling(c => c.Delete(1))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .Data(data => data
                    .WithSet<OfferedService>(s => s.Count() == 0))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GLOBAL_MESSAGE_WARNING_KEY))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<OfferedServicesController>(c => c.All()));
    }
}
