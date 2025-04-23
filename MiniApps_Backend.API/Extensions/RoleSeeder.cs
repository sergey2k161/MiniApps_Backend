using MiniApps_Backend.DataBase.Extension;

namespace MiniApps_Backend.API.Extensions
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            try
            {
                await RoleInitializer.SeedRoles(services);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while seeding roles: {ex.Message}");
            }
        }
    }

}
