using System;
using ELearn.Domain;

namespace ELearn.Infrastructure.Entity.Models
{
    public sealed class Review
    {
        public Guid Id { get;  set; }
        public Guid UserId { get;  set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } // 1 .. 5

        public Domain.Review ToModel()
        {
            return new Domain.Review(Id, UserId, Title, Comment, Rating);
        }
        
        
    }
    
    
}