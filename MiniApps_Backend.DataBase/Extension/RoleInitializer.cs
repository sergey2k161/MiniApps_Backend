using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace MiniApps_Backend.DataBase.Extension
{
    public static class RoleInitializer
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            string[] roles = { "Admin", "Analyst", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                    Console.WriteLine($"Role {role} created successfully.");
                }
                else
                {
                    Console.WriteLine($"Role {role} already exists.");
                }
            }
        }
    }
}
