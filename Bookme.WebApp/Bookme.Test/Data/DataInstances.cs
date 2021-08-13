using Bookme.Data.Models;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Bookme.Test.Data
{
    public static class DataInstances
    {
        public static OfferedService OneOfferedService
            => new OfferedService { UserId = TestUser.Identifier, ServiceCategoryId = 6 };

        public static Business OneBusiness
            => new Business { UserId = TestUser.Identifier, BusinessName = "BusinessTest" };

        public static ServiceCategory Category
           => new ServiceCategory
           {
               Id = 1,
               Name = "Category",
               ImageUrl = "image",
           };

        public static VisitationType VisitationType
           =>  new VisitationType
           {
               Id = 1,
               Type = "Type"
           };


    }
}
