using System;
using ELearn.Domain;

namespace ELearn.Infrastructure.Entity.Models
{
    public sealed class Review
    {
        public Review()
        {
        }

        public Guid Id { get;  set; }
        public Guid UserId { get;  set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public UserLevel RecommendFor { get; set; } // 1 .. 5

        public Domain.Review ToModel()
        {
            return new Domain.Review()
            {
                Id = Id,
                Comment = Comment,
                CreatedDate = CreatedDate,
                Title = Title,
                RecommendFor = RecommendFor
            };
        }
        
        
    }
    
    
}