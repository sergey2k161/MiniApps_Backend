using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Configurations_;
using MiniApps_Backend.DataBase.Models.Entity;

namespace MiniApps_Backend.DataBase
{
    public class MaDbContext : DbContext
    {
        public MaDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
