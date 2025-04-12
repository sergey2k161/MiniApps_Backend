using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Models.Entity.Ammount;

namespace MiniApps_Backend.DataBase.Configurations_
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder
                .HasKey(t => t.Id);

            builder
                .Property(u => u.PercentageDiscounts)
                .HasDefaultValue(0.0);

            builder
                .Property(u => u.WithDiscount)
                .HasDefaultValue(false);
        }
    }
}
