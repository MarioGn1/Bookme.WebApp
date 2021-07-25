using Bookme.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bookme.WebApp.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal userClaim)
            => userClaim.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static async Task<bool> IsInRole(this ClaimsPrincipal userClaim, UserManager<ApplicationUser> userManager, string role)
        {
            var user = await GetUser(userClaim, userManager);

            return await userManager.IsInRoleAsync(user, role);
        }

        public static async Task AddToRole(this ClaimsPrincipal userClaim, UserManager<ApplicationUser> userManager, string role)
        {
            var user = await GetUser(userClaim, userManager);

            await userManager.AddToRoleAsync(user, role);
        }

        private static async Task<ApplicationUser> GetUser(ClaimsPrincipal userClaim, UserManager<ApplicationUser> userManager)
        {
            var userId = userClaim.GetId();
            var user = await userManager.FindByIdAsync(userId);

            return user;
        }
    }
}
