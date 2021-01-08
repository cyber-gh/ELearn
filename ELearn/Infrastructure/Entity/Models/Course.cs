using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ELearn.Domain;

namespace ELearn.Infrastructure.Entity.Models
{
    public sealed class Course
    {
        public Course(Guid id, string title, string previewImageUrl, string description, int length, UserLevel userLevel, ICollection<Category> categories, Guid authorId)
        {
            Id = id;
            Title = title;
            PreviewImageUrl = previewImageUrl;
            Description = description;
            Length = length;
            UserLevel = userLevel;
            Categories = categories;
            AuthorId = authorId;
        }

        public Course(Guid id, string title, string previewImageUrl, string description, int length, UserLevel userLevel, Guid authorId)
        {
            Id = id;
            Title = title;
            PreviewImageUrl = previewImageUrl;
            Description = description;
            Length = length;
            UserLevel = userLevel;
            AuthorId = authorId;
            Categories = new List<Category>();
        }

        public Course()
        {
            Categories = new HashSet<Category>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string PreviewImageUrl { get; set; }
        public String Description { get; set; }
        public int Length { get; set; } //seconds total
        public UserLevel UserLevel { get; set; }
        public Guid AuthorId { get; set; }
        public ICollection<Category> Categories { get; set; }

        public Domain.CourseOverview ToModel()
        {
            //var category = Category == null ? null : new Domain.Category(Category.Id, Category.Name);
            var categories = new List<Domain.Category>();
            if (Categories != null)
            {
                foreach (var category in Categories)
                {
                    categories.Add(category.ToModel());
                }
            }

            return new Domain.CourseOverview(
                Id,
                Title,
                PreviewImageUrl,
                Description,
                Length,
                UserLevel,
                categories,
                new AppUser()
            );
        }
    }
}