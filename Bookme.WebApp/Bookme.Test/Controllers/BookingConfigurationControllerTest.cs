using Bookme.Data.Models;
using Bookme.WebApp.Controllers;
using Bookme.ViewModels.BookingConfiguration;
using MyTested.AspNetCore.Mvc;
using Xunit;
using System;
using System.Linq;

using static Bookme.Test.Data.User;
using static Bookme.Test.Data.DataInstances;
using static Bookme.WebApp.Controllers.Constants.RoleConstants;
using static Bookme.WebApp.Controllers.Constants.TempDataConstants;

namespace Bookme.Test.Controllers
{
    public class BookingConfigurationControllerTest
    {
        [Theory]
        [InlineData("Mario@abv.bg", CLIENT)]
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
                .Redirect(redirect => redirect
                    .To<BusinessController>(c => c.Create()));

        [Fact]
        public void GetShiftInfoShouldBeForAuthorizedUsersAndReturnView()
            => MyController<BookingConfigurationController>
                .Instance(c => c
                    .WithUser(OneUser.UserName, new[] { BUSINESS })
                    .WithData(
                        OneUser,
                        BusinessIdentityRole,
                        BusinessUserRole,
                        OneBusiness,
                        OneBookingConfiguration,
                        OneWeeklySchedule,
                        OneBreakTemplate
                    ))
                .Calling(c => c.ShiftInfo())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ConfigureBookingConfigurationViewModel>());

        [Theory]
        [InlineData("Mario@abv.bg", CLIENT)]
        public void postShiftInfoShouldBeForAuthorizedUsersAndRedirect(string username, string role)
            => MyController<BookingConfigurationController>
                .Instance(c => c
                    .WithUser(username, new[] { role })
                    .WithData(OneUser, ClientIdentityRole, ClientUserRole))
                .Calling(c => c.ShiftInfo(new ConfigureBookingConfigurationViewModel()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<BusinessController>(c => c.Create()));

        [Theory]
        [InlineData("Mario@abv.bg", BUSINESS)]
        public void postShiftInfoShouldBeForAuthorizedUsersAndReturnViewWithInvalidModel(string username, string role)
            => MyController<BookingConfigurationController>
                .Instance(c => c
                    .WithUser(username, new[] { role })
                    .WithData(OneUser, BusinessIdentityRole, BusinessUserRole))
                .Calling(c => c.ShiftInfo(new ConfigureBookingConfigurationViewModel()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ConfigureBookingConfigurationViewModel>());

        [Theory]
        [InlineData(1, "08:30", "16:30", 60, true, "12:00", "13:00")]
        public void PostEditShouldBeForAuthorizedUsersAndRedirectWithValidModel(
            int id,
            string shiftStart,
            string shiftEnd,
            int serviceInterval,
            bool monday,
            string breakStart,
            string breakEnd
            )
            => MyController<BookingConfigurationController>
                .Instance(c => c
                    .WithData(
                        OneUser,
                        BusinessIdentityRole,
                        BusinessUserRole,
                        OneBusiness,
                        OneBookingConfiguration,
                        OneWeeklySchedule,
                        OneBreakTemplate
                    )
                    .WithUser(OneUser.UserName, new[] { BUSINESS }))
                .Calling(c => c.ShiftInfo(new ConfigureBookingConfigurationViewModel
                {
                    Id = id,
                    ShiftStart = TimeSpan.Parse(shiftStart),
                    ShiftEnd = TimeSpan.Parse(shiftEnd),
                    ServiceInterval = serviceInterval,
                    WeeklySchedule = new WeeklyScheduleViewModel { Monday = monday },
                    Breaks = new BreaksViewModel 
                    { 
                        FirstBreak = new PartialBreakViewModel 
                        { 
                            BreakStart = TimeSpan.Parse(breakStart),
                            BreakEnd = TimeSpan.Parse(breakEnd),
                        },
                        SecondBreak = new PartialBreakViewModel(),
                        ThirdBreak = new PartialBreakViewModel(),
                    }
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<BookingConfiguration>(configs => configs
                        .Any(s =>
                        s.Id == id &&
                        s.ShiftStart == TimeSpan.Parse(shiftStart) &&
                        s.ShiftEnd == TimeSpan.Parse(shiftEnd) &&
                        s.ServiceInterval == serviceInterval &&
                        s.WeeklySchedule.Monday == true &&
                        s.Breaks.Any(br => br.BreakStart == TimeSpan.Parse(breakStart) && 
                            br.BreakEnd == TimeSpan.Parse(breakEnd))
                        )))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GLOBAL_MESSAGE_KEY))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<BookingConfigurationController>(c => c.ShiftInfo()));

        [Theory]
        [InlineData(1, "08:30", "16:30", 60, true, "12:00", "13:00")]
        public void PostEditShouldBeForAuthorizedUsersAndRedirectHomeWhenConfigurationNotExistent(
            int id,
            string shiftStart,
            string shiftEnd,
            int serviceInterval,
            bool monday,
            string breakStart,
            string breakEnd
            )
            => MyController<BookingConfigurationController>
                .Instance(c => c
                    .WithData(
                        OneUser,
                        BusinessIdentityRole,
                        BusinessUserRole,
                        OneBusiness
                    )
                    .WithUser(OneUser.UserName, new[] { BUSINESS }))
                .Calling(c => c.ShiftInfo(new ConfigureBookingConfigurationViewModel
                {
                    Id = id,
                    ShiftStart = TimeSpan.Parse(shiftStart),
                    ShiftEnd = TimeSpan.Parse(shiftEnd),
                    ServiceInterval = serviceInterval,
                    WeeklySchedule = new WeeklyScheduleViewModel { Monday = monday },
                    Breaks = new BreaksViewModel
                    {
                        FirstBreak = new PartialBreakViewModel
                        {
                            BreakStart = TimeSpan.Parse(breakStart),
                            BreakEnd = TimeSpan.Parse(breakEnd),
                        },
                        SecondBreak = new PartialBreakViewModel(),
                        ThirdBreak = new PartialBreakViewModel(),
                    }
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GLOBAL_MESSAGE_DANGER_KEY))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<HomeController>(c => c.Index()));

        [Theory]
        [InlineData("Mario@abv.bg", CLIENT)]
        public void GetConfigureInfoShouldBeForAuthorizedUsersAndRedirect(string username, string role)
            => MyController<BookingConfigurationController>
                .Instance(c => c
                    .WithUser(username, new[] { role })
                    .WithData(OneUser, ClientIdentityRole, ClientUserRole))
                .Calling(c => c.Configure())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<BusinessController>(c => c.Create()));

        [Fact]
        public void GetConfigureInfoShouldBeForAuthorizedUsersAndReturnView()
            => MyController<BookingConfigurationController>
                .Instance(c => c
                    .WithUser(OneUser.UserName, new[] { BUSINESS })
                    .WithData(OneUser, BusinessIdentityRole, BusinessUserRole))
                .Calling(c => c.Configure())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("Mario@abv.bg", CLIENT)]
        public void PostConfigureInfoShouldBeForAuthorizedUsersAndRedirect(string username, string role)
            => MyController<BookingConfigurationController>
                .Instance(c => c
                    .WithUser(username, new[] { role })
                    .WithData(OneUser, ClientIdentityRole, ClientUserRole))
                .Calling(c => c.Configure(new ConfigureBookingConfigurationViewModel()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<BusinessController>(c => c.Create()));

        [Theory]
        [InlineData("Mario@abv.bg", BUSINESS)]
        public void PostConfigureInfoShouldBeForAuthorizedUsersAndReturnViewWithInvalidModel(string username, string role)
            => MyController<BookingConfigurationController>
                .Instance(c => c
                    .WithUser(username, new[] { role })
                    .WithData(OneUser, BusinessIdentityRole, BusinessUserRole))
                .Calling(c => c.Configure(new ConfigureBookingConfigurationViewModel()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ConfigureBookingConfigurationViewModel>());

        [Theory]
        [InlineData("08:30", "16:30", 60, true, "12:00", "13:00")]
        public void PostConfigureShouldBeForAuthorizedUsersAndRedirectWithValidModel(
            string shiftStart,
            string shiftEnd,
            int serviceInterval,
            bool monday,
            string breakStart,
            string breakEnd
            )
            => MyController<BookingConfigurationController>
                .Instance(c => c
                    .WithData(
                        OneUser,
                        BusinessIdentityRole,
                        BusinessUserRole,
                        BusinessWithoutBookingConfiguration
                    )
                    .WithUser(OneUser.UserName, new[] { BUSINESS }))
                .Calling(c => c.Configure(new ConfigureBookingConfigurationViewModel
                {
                    ShiftStart = TimeSpan.Parse(shiftStart),
                    ShiftEnd = TimeSpan.Parse(shiftEnd),
                    ServiceInterval = serviceInterval,
                    WeeklySchedule = new WeeklyScheduleViewModel { Monday = monday },
                    Breaks = new BreaksViewModel
                    {
                        FirstBreak = new PartialBreakViewModel
                        {
                            BreakStart = TimeSpan.Parse(breakStart),
                            BreakEnd = TimeSpan.Parse(breakEnd),
                        },
                        SecondBreak = new PartialBreakViewModel(),
                        ThirdBreak = new PartialBreakViewModel(),
                    }
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<BookingConfiguration>(configs => configs
                        .Any(s =>
                        s.ShiftStart == TimeSpan.Parse(shiftStart) &&
                        s.ShiftEnd == TimeSpan.Parse(shiftEnd) &&
                        s.ServiceInterval == serviceInterval &&
                        s.WeeklySchedule.Monday == true &&
                        s.Breaks.Any(br => br.BreakStart == TimeSpan.Parse(breakStart) &&
                            br.BreakEnd == TimeSpan.Parse(breakEnd))
                        )))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GLOBAL_MESSAGE_KEY))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<OfferedServicesController>(c => c.All()));

        [Theory]
        [InlineData("08:30", "16:30", 60, true, "12:00", "13:00")]
        public void PostConfigureShouldBeForAuthorizedUsersAndRedirectWhenAlreadyConfigured(
            string shiftStart,
            string shiftEnd,
            int serviceInterval,
            bool monday,
            string breakStart,
            string breakEnd
            )
            => MyController<BookingConfigurationController>
                .Instance(c => c
                    .WithData(
                        OneUser,
                        BusinessIdentityRole,
                        BusinessUserRole,
                        OneBusiness
                    )
                    .WithUser(OneUser.UserName, new[] { BUSINESS }))
                .Calling(c => c.Configure(new ConfigureBookingConfigurationViewModel
                {
                    ShiftStart = TimeSpan.Parse(shiftStart),
                    ShiftEnd = TimeSpan.Parse(shiftEnd),
                    ServiceInterval = serviceInterval,
                    WeeklySchedule = new WeeklyScheduleViewModel { Monday = monday },
                    Breaks = new BreaksViewModel
                    {
                        FirstBreak = new PartialBreakViewModel
                        {
                            BreakStart = TimeSpan.Parse(breakStart),
                            BreakEnd = TimeSpan.Parse(breakEnd),
                        },
                        SecondBreak = new PartialBreakViewModel(),
                        ThirdBreak = new PartialBreakViewModel(),
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
                .Redirect(redirect => redirect
                    .To<BookingConfigurationController>(c => c.ShiftInfo()));
    }
}
