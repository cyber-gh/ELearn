using System;

namespace ELearn.Domain
{
    public sealed class Review: IEntity
    {
        public Review(Guid id, Guid userId, string title, string comment, int rating)
        {
            Id = id;
            UserId = userId;
            Title = title;
            Comment = comment;
            Rating = rating;
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } // 1 .. 5
    }
}