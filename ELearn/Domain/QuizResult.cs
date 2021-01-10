using System;
using System.Collections.Generic;

namespace ELearn.Domain
{
    public class QuizResult: IEntity
    {
        public Guid Id { get; set; }
        public AppUser? User { get; set; }
        public IEnumerable<QuizResultElement> Elements { get; set; }
        public class QuizResultElement
        {
            public string Question { get; set; }
            public string Answer { get; set; }
            public string CorrectAnswer { get; set; }
        }

    }
}