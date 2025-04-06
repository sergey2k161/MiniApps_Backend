using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Models.Entity;

namespace MiniApps_Backend.DataBase.Configurations_
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.Id);

            builder
                .HasIndex(u => u.TelegramId)
                .IsUnique();
        }
    }
}
