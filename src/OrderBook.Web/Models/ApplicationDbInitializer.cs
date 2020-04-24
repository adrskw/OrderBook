using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OrderBook.Web.Models
{
    public static class ApplicationDbInitializer
    {
        public static void SeedInitialData(RoleManager<IdentityRole> roleManager,
                                                 UserManager<ApplicationUser> userManager)
        {
            SeedAdminRole(roleManager);
            SeedAdminUser(userManager);
        }

        private static void SeedAdminRole(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };

                roleManager.CreateAsync(role).Wait();
            }
        }

        private static void SeedAdminUser(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null && userManager.GetUsersInRoleAsync("admin").Result.Count == 0)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    FirstName = "Admin",
                    LastName = "Admin"
                };

                IdentityResult result = userManager.CreateAsync(user, "zmiendomyslnehaslo").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}