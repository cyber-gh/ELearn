using System;
using System.Collections.Generic;

namespace ELearn.Domain
{
    public sealed class Quiz: IEntity
    {
        public Guid Id { get; private set; }
        public string Question { get; set; }
        public IEnumerable<String> Answers { get; set; }
        public String CorrectAnswer { get; set; }
    }
}