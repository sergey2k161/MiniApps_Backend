﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
                .HasMany(c => c.Blocks)
                .WithOne(b => b.Course)
                .HasForeignKey(b => b.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(c => c.Users)
                .WithMany(u => u.Courses)
                .UsingEntity<CourseSubscription>(
                    j => j
                        .HasOne(cs => cs.User)
                        .WithMany()
                        .HasForeignKey(cs => cs.TelegramId)
                        .HasPrincipalKey(u => u.TelegramId),
                    j => j
                        .HasOne(cs => cs.Course)
                        .WithMany()
                        .HasForeignKey(cs => cs.CourseId)
                );
        }
    }
}
