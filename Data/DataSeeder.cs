using Microsoft.AspNetCore.Identity;
using MoviesApplication.Data.Static;
using MoviesApplication.Models;

namespace MoviesApplication.Data
{
    public class DataSeeder
    {
        public static async void UserRolesSeed(IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //role
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if(!await roleManager.RoleExistsAsync(UserRoles.ADMIN))
                {
                   await roleManager.CreateAsync(new IdentityRole(UserRoles.ADMIN));
                }
                if (!await roleManager.RoleExistsAsync(UserRoles.USER))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.USER));
                }
                //user
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                //normal user
                var userEmail = "user@user.com";
                var normalUser = await userManager.FindByEmailAsync(userEmail);
                if (normalUser == null) {
                    var newNormalUser = new ApplicationUser()
                    {
                        UserName = "User",
                        Email = userEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newNormalUser, "User@123");
                    await userManager.AddToRoleAsync(newNormalUser, UserRoles.USER);
                }
                //admin user
                var adminEmail = "admin@admin.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        UserName = "Admin",
                        Email = adminEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin@123");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.ADMIN);
                }
            }   
        }
    }
}
