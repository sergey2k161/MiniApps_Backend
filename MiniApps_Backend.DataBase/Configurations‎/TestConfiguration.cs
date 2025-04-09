using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;

namespace MiniApps_Backend.DataBase.Configurations_
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder
                .HasKey(t => t.Id);

            builder
                .HasOne(t => t.Lesson)
                .WithOne(l => l.Test)
                .HasForeignKey<Lesson>(l => l.TestId);

            builder
                .HasMany(t => t.Questions)
                .WithOne(t => t.Test)
                .HasForeignKey(q => q.TestId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
