using System;
using System.Collections.Generic;

namespace ELearn.Domain
{
    public class Quiz: IEntity
    {
        public Guid Id { get; set; }
        public IEnumerable<QuizElement> Elements { get; set; }
    }
}