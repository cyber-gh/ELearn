using System;

namespace ELearn.Domain
{
    public sealed class CourseOverview: IEntity
    {
        public CourseOverview(Guid id, string title, string previewImageUrl, string description, int length, UserLevel userLevel, Category? category)
        {
            Id = id;
            Title = title;
            PreviewImageUrl = previewImageUrl;
            Description = description;
            Length = length;
            UserLevel = userLevel;
            Category = category;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string PreviewImageUrl { get; set; }
        public String Description { get; set; }
        public int Length { get; set; } //seconds total
        public UserLevel UserLevel { get; set; }
        public Category? Category { get; set; }
    }
}