using System;
using System.Collections.Generic;

namespace ELearn.Infrastructure.Entity.Models
{
    public sealed class Quiz
    {
        public Guid Id { get;  set; }
        public Guid LessonId { get; set; }
        public string Question { get; set; }
        public IEnumerable<QuizAnswer> Answers { get; set; }
        public Guid CorrectAnswer { get; set; }
    }
}