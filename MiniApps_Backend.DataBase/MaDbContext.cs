using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Configurations_;
using MiniApps_Backend.DataBase.Models.Entity;

namespace MiniApps_Backend.DataBase
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class MaDbContext : IdentityDbContext<CommonUser, IdentityRole<Guid>, Guid>
    {
        public MaDbContext(DbContextOptions<MaDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new TransactionConfiguration());
        }
    }
}
