using System;
using System.Collections.Generic;
using IdentityServer4.Extensions;

namespace ELearn.Domain
{
    public sealed class Course: IEntity
    {
        
        public Course(CourseOverview overview)
        {
            Id = overview.Id;
            Title = overview.Title;
            PreviewImageUrl = overview.PreviewImageUrl;
            Description = overview.Description;
            Length = overview.Length;
            UserLevel = overview.UserLevel;
            Reviews = new List<Review>();
            Lessons = new List<Lesson>();
        }
        public Course(CourseOverview overview, IEnumerable<Lesson> lessons, IEnumerable<Review> reviews)
        {
            Id = overview.Id;
            Title = overview.Title;
            PreviewImageUrl = overview.PreviewImageUrl;
            Description = overview.Description;
            Length = overview.Length;
            UserLevel = overview.UserLevel;
            Reviews = reviews;
            Lessons = lessons;
        }

        public CourseOverview Overview => new CourseOverview(Id, Title, PreviewImageUrl, Description, Length, UserLevel);

        public Guid Id { get; private set; }
        public string Title { get; set; }
        public string PreviewImageUrl { get; set; }
        public String Description { get; set; }
        public int Length { get; set; } //seconds total
        public UserLevel UserLevel { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }
        
    }
}