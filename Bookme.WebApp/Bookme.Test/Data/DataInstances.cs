using Bookme.Data.Models;
using MyTested.AspNetCore.Mvc;
using System;
using static Bookme.Test.Data.User;

namespace Bookme.Test.Data
{
    public static class DataInstances
    {
        public static OfferedService OneOfferedService
            => new OfferedService
            {
                Id = 1,
                UserId = TestUser.Identifier,
                ServiceCategoryId = 1,
                Duration = 60,
                Name = "OfferedServiceName"
            };

        public static Business OneBusiness
            => new Business
            {
                Id = 1,
                UserId = TestUser.Identifier,
                BusinessName = "BusinessTest",
                BookingConfigurationId = 1
            };

        public static Business BusinessWithoutBookingConfiguration
            => new Business
            {
                Id = 1,
                UserId = TestUser.Identifier,
                BusinessName = "BusinessTest",
            };

        public static BookingConfiguration OneBookingConfiguration
            => new BookingConfiguration
            {
                Id = 1,
                ServiceInterval = 40
            };

        public static WeeklySchedule OneWeeklySchedule
            => new WeeklySchedule
            {
                Id = 1,
                BookingConfigurationId = 1
            };

        public static BreakTemplate OneBreakTemplate
            => new BreakTemplate
            {
                Id = 1,
                BookingConfigurationId = 1
            };

        public static ServiceCategory OneCategory
       => new ServiceCategory
       {
           Id = 1,
           Name = "Category",
           ImageUrl = "image",
       };

        public static VisitationType OneVisitationType
           => new VisitationType
           {
               Id = 1,
               Type = "Type"
           };

        public static ServiceVisitation OneServiceVisitation
            => new ServiceVisitation
            {
                OfferedServiceId = 1,
                VisitationTypeId = 1
            };

        public static Booking OneBooking
            => new Booking
            {
                Id = 1,
                ClientId = SecondUser.Id,
                Date = DateTime.Parse("2021-06-07 10:30:00"),
                Duration = 60,
            };
    }
}
