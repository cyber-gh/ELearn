using System;
using System.Collections.Generic;

namespace ELearn.Domain
{
    public sealed class Quiz: IEntity
    {
        public Quiz(Guid id, string question, IEnumerable<string> answers, string correctAnswer)
        {
            Id = id;
            Question = question;
            Answers = answers;
            CorrectAnswer = correctAnswer;
        }

        public Guid Id { get; private set; }
        public string Question { get; set; }
        public IEnumerable<String> Answers { get; set; }
        public String CorrectAnswer { get; set; }
    }
}