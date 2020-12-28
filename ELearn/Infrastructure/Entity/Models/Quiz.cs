using System;
using System.Collections.Generic;
using System.Linq;

namespace ELearn.Infrastructure.Entity.Models
{
    public sealed class Quiz
    {
        public Quiz()
        {
        }

        public Quiz(Guid id, Guid lessonId, string question, IEnumerable<QuizAnswer> answers, Guid correctAnswer)
        {
            Id = id;
            LessonId = lessonId;
            Question = question;
            Answers = answers;
            CorrectAnswer = correctAnswer;
        }

        public Guid Id { get;  set; }
        public Guid LessonId { get; set; }
        public string Question { get; set; }
        public IEnumerable<QuizAnswer> Answers { get; set; }
        public Guid CorrectAnswer { get; set; }

        public Domain.Quiz ToModel()
        {
            return new Domain.Quiz(Id, Question, Answers.Select(p => p.Answer).ToList(),
                Answers.FirstOrDefault(p => p.Id == CorrectAnswer)?.Answer ?? string.Empty);
        }
    }
}