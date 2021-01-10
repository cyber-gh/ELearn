using System;
using System.Collections;
using System.Collections.Generic;

namespace ELearn.Domain
{
    public sealed class CourseOverview: IEntity
    {
        public CourseOverview(Guid id, string title, string previewImageUrl, string description, int length, UserLevel userLevel, IEnumerable<Category> categories, AppUser appUser)
        {
            Id = id;
            Title = title;
            PreviewImageUrl = previewImageUrl;
            Description = description;
            Length = length;
            UserLevel = userLevel;
            Categories = categories;
            AppUser = appUser;
        }
        
        public CourseOverview(Guid id, string title, string previewImageUrl, string description, int length, UserLevel userLevel)
        {
            Id = id;
            Title = title;
            PreviewImageUrl = previewImageUrl;
            Description = description;
            Length = length;
            UserLevel = userLevel;
            Visitors = 0;
            AppUser = new AppUser();
            Categories = new List<Category>();
        }

        public CourseOverview(Guid id, string title, string previewImageUrl, string description, int length, UserLevel userLevel, AppUser appUser)
        {
            Id = id;
            Title = title;
            PreviewImageUrl = previewImageUrl;
            Description = description;
            Length = length;
            UserLevel = userLevel;
            AppUser = appUser;
            Visitors = 0;
            Categories = new List<Category>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string PreviewImageUrl { get; set; }
        public String Description { get; set; }
        public int Length { get; set; } //seconds total
        public UserLevel UserLevel { get; set; }
        public int Visitors { get; set; }
        public AppUser AppUser { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}