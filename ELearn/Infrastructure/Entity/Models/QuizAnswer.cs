using System;

namespace ELearn.Infrastructure.Entity.Models
{
    public class QuizAnswer
    {
    
        public Guid Id { get; set; }
        
        public Guid QuizId { get; set; }

        public string Answer { get; set; }
    }
}