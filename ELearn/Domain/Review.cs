using System;

namespace ELearn.Domain
{
    public sealed class Review: IEntity
    {
        public Review()
        {
        }

        public Review(Guid id, string title, string comment, UserLevel recommendFor)
        {
            Id = id;
            Title = title;
            Comment = comment;
            RecommendFor = recommendFor;
        }

        public Review(Guid id, AppUser user, string title, string comment, DateTime createdDate, UserLevel recommendFor)
        {
            Id = id;
            User = user;
            Title = title;
            Comment = comment;
            CreatedDate = createdDate;
            RecommendFor = recommendFor;
        }

        public Guid Id { get; set; }
        public AppUser User { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public UserLevel RecommendFor { get; set; } // 1 .. 5
    }
}