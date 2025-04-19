using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Models.Entity;

namespace MiniApps_Backend.DataBase.Configurations_
{
    public class SupportConfiguration : IEntityTypeConfiguration<Support>
    {
        public void Configure(EntityTypeBuilder<Support> builder)
        {
            builder
                .HasKey(u => u.Id);      

            builder
                .Property(u => u.InWork)
                .HasDefaultValue(false);

            builder
                .Property(u => u.Process)
                .HasDefaultValue("Новое");
        }
    }
}
