using Bookme.Data.Models;
using MyTested.AspNetCore.Mvc;

namespace Bookme.Test.Data
{
    public static class User
    {
        public static ApplicationUser OneUser
            => new ApplicationUser { Id = TestUser.Identifier, UserName = "Mario@abv.bg", Email = "Mario@abv.bg" };
    }
}
