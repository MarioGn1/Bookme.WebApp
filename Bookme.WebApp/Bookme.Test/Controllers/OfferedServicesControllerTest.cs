using Bookme.WebApp.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

using static Bookme.Test.Data.User;
using static Bookme.Test.Data.DataInstances;
using static Bookme.WebApp.Controllers.Constants.RoleConstants;
using System.Collections.Generic;
using Bookme.ViewModels.OfferedServices;

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
    }
}
