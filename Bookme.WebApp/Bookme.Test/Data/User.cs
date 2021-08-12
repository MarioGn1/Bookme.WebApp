using Bookme.Data.Models;
using Bookme.WebApp.Controllers.Constants;
using Microsoft.AspNetCore.Identity;
using MyTested.AspNetCore.Mvc;

namespace Bookme.Test.Data
{
    public static class User
    {
        public static ApplicationUser OneUser
            => new ApplicationUser { Id = TestUser.Identifier, UserName = "Mario@abv.bg", Email = "Mario@abv.bg", NormalizedUserName = "MARIO@ABV.BG", SecurityStamp = "TestStamp" };

        public static IdentityRole ClientIdentityRole
            => new IdentityRole { Id = "TestRole", Name = "Client", NormalizedName = "CLIENT" };

        public static IdentityRole BusinessIdentityRole
    => new IdentityRole { Id = "TestRoleBusiness", Name = "Business", NormalizedName = "BUSINESS" };

        public static IdentityUserRole<string> ClientUserRole
            => new IdentityUserRole<string> { RoleId = ClientIdentityRole.Id, UserId = OneUser.Id };

        public static IdentityUserRole<string> BusinessUserRole
            => new IdentityUserRole<string> { RoleId = BusinessIdentityRole.Id, UserId = OneUser.Id };
    }
}
