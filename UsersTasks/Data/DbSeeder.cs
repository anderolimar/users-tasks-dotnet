using Microsoft.AspNetCore.Identity;
using UsersTasks.Interfaces;
using UsersTasks.Models.Auth;

namespace UsersTasks.Data
{
    public class DbSeeder
    {
        public static async Task Seed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<DbSeeder>>();

            try
            {
                var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                if (userManager != null && userManager.Users.Any() == false)
                {
                    var adminuser = new ApplicationUser
                    {
                        Name = "Admin",
                        UserName = "admin@test.com",
                        Email = "admin@test.com",
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    if (roleManager != null && (await roleManager.RoleExistsAsync(Roles.Admin)) == false)
                    {
                        logger.LogInformation("Admin role is creating");
                        var roleResult = await roleManager
                          .CreateAsync(new IdentityRole(Roles.Admin));

                        if (roleResult.Succeeded == false)
                        {
                            var roleErros = roleResult.Errors.Select(e => e.Description);
                            logger.LogError($"Failed to create admin role. Errors : {string.Join(",", roleErros)}");

                            return;
                        }
                        logger.LogInformation("Admin role is created");
                    }

                    var createAdminUserResult = await userManager
                          .CreateAsync(user: adminuser, password: "Admin@123");

                    if (createAdminUserResult.Succeeded == false)
                    {
                        var errors = createAdminUserResult.Errors.Select(e => e.Description);
                        logger.LogError(
                            $"Failed to create admin user. Errors: {string.Join(", ", errors)}"
                        );
                        return;
                    }

                    var addAdminUserToRoleResult = await userManager
                                    .AddToRoleAsync(user: adminuser, role: Roles.Admin);

                    if (addAdminUserToRoleResult.Succeeded == false)
                    {
                        var errors = addAdminUserToRoleResult.Errors.Select(e => e.Description);
                        logger.LogError($"Failed to add admin role to user. Errors : {string.Join(",", errors)}");
                    }
                    logger.LogInformation("Admin user is created");



                    // Create User role if it doesn't exist
                    if (roleManager != null && (await roleManager.RoleExistsAsync(Roles.User)) == false)
                    {
                        var roleResult = await roleManager
                              .CreateAsync(new IdentityRole(Roles.User));

                        if (roleResult.Succeeded == false)
                        {
                            var roleErros = roleResult.Errors.Select(e => e.Description);
                            logger.LogError($"Failed to create user role. Errors : {string.Join(",", roleErros)}");
                        }
                    }

                    ApplicationUser user = new()
                    {
                        Email = "user@test.com",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserName = "user@test.com",
                        Name = "User",
                        EmailConfirmed = true
                    };

                    // Attempt to create a user
                    var createUserResult = await userManager.CreateAsync(user, "User@123");
                    if (createUserResult.Succeeded == false)
                    {
                        var errors = createUserResult.Errors.Select(e => e.Description);
                        logger.LogError(
                            $"Failed to create user. Errors: {string.Join(", ", errors)}"
                        );
                    }

                    var addUserToRoleResult = await userManager.AddToRoleAsync(user: user, role: Roles.User);
                    if (addUserToRoleResult.Succeeded == false)
                    {
                        var errors = addUserToRoleResult.Errors.Select(e => e.Description);

                        logger.LogError($"Failed to add role to the user. Errors : {string.Join(",", errors)}");
                    }
                }
            }

            catch (Exception ex)
            {
                logger.LogCritical(ex.Message);
            }

        }
    }
}
