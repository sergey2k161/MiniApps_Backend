using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Models.Entity;

namespace MiniApps_Backend.DataBase.Configurations_
{
    /// <summary>
    /// Конфигурация БД пользователя
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.Id);

            builder
                .HasOne(u => u.CommonUser)
                .WithOne(c => c.User)
                .HasForeignKey<CommonUser>(c => c.UserId);

            builder
                .HasOne(u => u.Wallet)
                .WithOne(w => w.User)
                .HasForeignKey<Wallet>(c => c.UserId);

            builder
                .HasIndex(u => u.TelegramId)
                .IsUnique();

            builder
                .Property(u => u.Experience)
                .HasDefaultValue(0);

            builder
                .Property(u => u.Level)
                .HasDefaultValue(1);
        }
    }
}
