using System;
using System.Collections.Generic;
using System.Linq;

namespace ELearn.Infrastructure.Entity.Models
{
    public sealed class QuizElement
    {
        public QuizElement(Guid id, Guid quizId, string question, List<string> answers, string correctAnswer)
        {
            Id = id;
            Question = question;
            Answers = answers;
            CorrectAnswer = correctAnswer;
            QuizId = quizId;
        }

      

        public Guid Id { get;  set; }
        public Guid QuizId { get;  set; }
        public string Question { get; set; }
        public List<String> Answers { get; set; }
        public String CorrectAnswer { get; set; }

       
    }
}