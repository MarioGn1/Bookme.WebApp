using Bookme.Data.Models;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Bookme.Test.Data
{
    public static class DataInstances
    {
        public static OfferedService OneOfferedService
            => new OfferedService
            {
                Id = 1,
                UserId = TestUser.Identifier,
                ServiceCategoryId = 1
            };

        public static Business OneBusiness
            => new Business
            {
                Id = 1,
                UserId = TestUser.Identifier,
                BusinessName = "BusinessTest",
                BookingConfigurationId = 1
            };

        public static BookingConfiguration OneBookingConfiguration
            => new BookingConfiguration
            {
                Id = 1,
                ServiceInterval = 40
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

    }
}
