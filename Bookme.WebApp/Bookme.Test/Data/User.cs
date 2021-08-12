using Bookme.Data.Models;
using Bookme.WebApp.Controllers.Constants;
using Microsoft.AspNetCore.Identity;
using MyTested.AspNetCore.Mvc;

namespace Bookme.Test.Data
{
    public static class User
    {
        public static ApplicationUser OneUser
            => new ApplicationUser { Id = TestUser.Identifier, UserName = "Mario@abv.bg", Email = "Mario@abv.bg" };

        public static IdentityRole ClientIdentityRole
            => new IdentityRole ("Client");
        public static RoleConstants Constants
            => new RoleConstants();
    }
}
