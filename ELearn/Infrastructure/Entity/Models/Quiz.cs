using System;
using System.Collections;
using System.Collections.Generic;

namespace ELearn.Infrastructure.Entity.Models
{
    public class Quiz
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<QuizElement> Elements { get; set; }

    }
}