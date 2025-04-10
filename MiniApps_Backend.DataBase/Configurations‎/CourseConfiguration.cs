using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity.ManyToMany;

namespace MiniApps_Backend.DataBase.Configurations_
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .HasMany(c => c.Lessons)
                .WithOne(l => l.Course)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(c => c.Users)
                .WithMany(u => u.Courses)
                .UsingEntity<CourseSubscription>(
                    j => j
                        .HasOne(cs => cs.User)
                        .WithMany()
                        .HasForeignKey(cs => cs.UserId),
                    j => j
                        .HasOne(cs => cs.Course)
                        .WithMany()
                        .HasForeignKey(cs => cs.CourseId)
                );
        }
    }
}
