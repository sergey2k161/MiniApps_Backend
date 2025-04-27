using MiniApps_Backend.DataBase.Extension;

namespace MiniApps_Backend.API.Extensions
{
    /// <summary>
    /// Класс для инициализации ролей в системе
    /// </summary>
    public static class RoleSeeder
    {
        /// <summary>
        /// Метод для инициализации ролей в системе
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
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
