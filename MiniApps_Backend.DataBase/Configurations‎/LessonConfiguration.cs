using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;

namespace MiniApps_Backend.DataBase.Configurations_
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder
                .HasKey(l => l.Id);

            builder
                .HasOne(l => l.Block)
                .WithMany(c => c.Lessons)
                .HasForeignKey(l => l.BlockId);
                //.OnDelete(DeleteBehavior.Cascade);

            //builder
            //    .HasOne(l => l.Test)
            //    .WithOne(t => t.Lesson)
            //    .HasForeignKey<Lesson>(l => l.TestId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
