using System;
using System.Collections;
using System.Collections.Generic;

namespace ELearn.Domain
{
    public sealed class CourseOverview: IEntity
    {
        public CourseOverview(Guid id, string title, string previewImageUrl, string description, int length, UserLevel userLevel, IEnumerable<Category> categories)
        {
            Id = id;
            Title = title;
            PreviewImageUrl = previewImageUrl;
            Description = description;
            Length = length;
            UserLevel = userLevel;
            Categories = categories;
        }

        public CourseOverview(Guid id, string title, string previewImageUrl, string description, int length, UserLevel userLevel)
        {
            Id = id;
            Title = title;
            PreviewImageUrl = previewImageUrl;
            Description = description;
            Length = length;
            UserLevel = userLevel;
            Categories = new List<Category>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string PreviewImageUrl { get; set; }
        public String Description { get; set; }
        public int Length { get; set; } //seconds total
        public UserLevel UserLevel { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}