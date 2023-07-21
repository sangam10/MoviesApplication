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

        public static async void seed(IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<MoviesAppContext>();
                context.Database.EnsureCreated();
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(
                        new List<Movie>()
                        {
                            new Movie()
                            {
                                Name = "First Movie",
                                Poster_Image = "https://www.placehold/500*500",
                                Short_Desc = "this is the movie description this is the movie description this is the movie description this is the movie description this is the movie description this is the movie description ",
                                Release_Date = new DateTime(2024, 12, 25),
                                ApplicationUserId = "179a1b7b-b920-46cf-8a46-6456ef1bf958"
                            },
                            new Movie()
                            {
                                Name = "First Movie",
                                Poster_Image = "https://www.placehold/500*500",
                                Short_Desc = "this is the movie description this is the movie description this is the movie description this is the movie description this is the movie description this is the movie description ",
                                Release_Date = new DateTime(2023, 12, 25),
                                ApplicationUserId = "179a1b7b-b920-46cf-8a46-6456ef1bf958"
                            },
                            new Movie()
                            {
                                Name = "First Movie",
                                Poster_Image = "https://www.placehold/500*500",
                                Short_Desc = "this is the movie description this is the movie description this is the movie description this is the movie description this is the movie description this is the movie description ",
                                Release_Date = new DateTime(2022, 12, 25),
                                ApplicationUserId = "179a1b7b-b920-46cf-8a46-6456ef1bf958"
                            },
                            new Movie()
                            {
                                Name = "First Movie",
                                Poster_Image = "https://www.placehold/500*500",
                                Short_Desc = "this is the movie description this is the movie description this is the movie description this is the movie description this is the movie description this is the movie description ",
                                Release_Date = new DateTime(2021, 12, 25),
                                ApplicationUserId = "179a1b7b-b920-46cf-8a46-6456ef1bf958"
                            }
                        }
                   );
                    context.SaveChanges();
                }
            };
        }
    }
}
