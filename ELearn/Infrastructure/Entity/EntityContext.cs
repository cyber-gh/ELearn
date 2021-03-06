using System;
using System.Linq;
using ELearn.Infrastructure.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;

namespace ELearn.Infrastructure.Entity
{
    public class EntityContext: DbContext
    {
        public EntityContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Models.Lesson> Lessons { get; set; }
        public DbSet<Models.Review> Reviews { get; set; }
        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Models.Course> Courses { get; set; }
        public DbSet<Models.UserCourse> UserCourses { get; set; }
        public DbSet<Models.Quiz> Quizzes { get; set; }
        public DbSet<Models.QuizElement> QuizElements { get; set; }
        
        // public DbSet<Models.CategoryCourse> CategoryCourses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Category>()
                .HasMany(p => p.Courses)
                .WithMany(p => p.Categories)
                .UsingEntity(j => j.ToTable("CategoryCourse"));

            modelBuilder.Entity<UserCourse>()
                .HasKey(c => new {c.UserId, c.CourseId});

            modelBuilder.Entity<QuizElement>()
                .Property(e => e.Answers)
                .HasConversion(
                    v => string.Join(';', v),
                    v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
            
        }
    }
}