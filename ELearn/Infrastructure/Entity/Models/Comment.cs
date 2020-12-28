using System;

namespace ELearn.Infrastructure.Entity.Models
{
    public sealed class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }
        
    }
}