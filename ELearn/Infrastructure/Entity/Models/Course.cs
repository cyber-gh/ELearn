using System;
using ELearn.Domain;

namespace ELearn.Infrastructure.Entity.Models
{
    public sealed class Course
    {
        public Course(Guid id, string title, string previewImageUrl, string description, int length, UserLevel userLevel, Category? category = null)
        {
            Id = id;
            Title = title;
            PreviewImageUrl = previewImageUrl;
            Description = description;
            Length = length;
            UserLevel = userLevel;
            Category = category;
        }

        public Course(Guid id, string title, string previewImageUrl, string description, int length, UserLevel userLevel)
        {
            Id = id;
            Title = title;
            PreviewImageUrl = previewImageUrl;
            Description = description;
            Length = length;
            UserLevel = userLevel;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string PreviewImageUrl { get; set; }
        public String Description { get; set; }
        public int Length { get; set; } //seconds total
        public UserLevel UserLevel { get; set; }
        public Category? Category { get; set; }

        public Domain.CourseOverview ToModel()
        {
            var category = Category == null ? null : new Domain.Category(Category.Id, Category.Name);
            
            return new Domain.CourseOverview(
                Id,
                Title,
                PreviewImageUrl,
                Description,
                Length,
                UserLevel,
                category
            );
        }
    }
}