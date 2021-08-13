using Bookme.Data.Models;
using Bookme.ViewModels.Categories;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Bookme.Test.Data
{
    public static class Category
    {
        public static OfferedService OneOfferedService
            => new OfferedService { UserId = TestUser.Identifier, ServiceCategoryId = 6 };

        public static Business OneBusiness
            => new Business { UserId = TestUser.Identifier, BusinessName = "BusinessTest" };

        
    }
}
