﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Configurations_;
using MiniApps_Backend.DataBase.Models.Dto.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Models.Entity.Ammount;
using MiniApps_Backend.DataBase.Models.Entity.Analysis;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity.ManyToMany;

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
        public DbSet<Course> Courses { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<CourseSubscription> CourseSubscriptions { get; set; }
        public DbSet<CourseMaterial> CourseMaterials { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<LessonResult> LessonResults { get; set; }
        public DbSet<Support> Supports { get; set; }
        public DbSet<RepliesReport> RepliesReports { get; set; }
        public DbSet<BotActionAnalytics> BotActionsAnalytics { get; set; }
        public DbSet<VisitLesson> VisitsLessons { get; set; }
        public DbSet<CourseSucsessDto> CourseResults { get; set; }
        public DbSet<BlockSucsessDto> BlocksSucsess { get; set; }
        public DbSet<VisitBlock> VisitsBlocks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new TransactionConfiguration());
            builder.ApplyConfiguration(new CourseConfiguration());
            builder.ApplyConfiguration(new LessonConfiguration());
            builder.ApplyConfiguration(new QuestionConfiguration());
            builder.ApplyConfiguration(new TestConfiguration());
            builder.ApplyConfiguration(new AnswerConfiguration());
            builder.ApplyConfiguration(new SupportConfiguration());
        }
    }
}
