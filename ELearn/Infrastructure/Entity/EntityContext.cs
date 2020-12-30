using ELearn.Infrastructure.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
        
        // public DbSet<Models.CategoryCourse> CategoryCourses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Category>()
                .HasMany(p => p.Courses)
                .WithMany(p => p.Categories)
                .UsingEntity(j => j.ToTable("CategoryCourse"));

        }
    }
}