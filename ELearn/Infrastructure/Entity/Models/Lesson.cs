using System;
using ELearn.Domain;

namespace ELearn.Infrastructure.Entity.Models
{
    public sealed class Lesson 
    {
        public Lesson(Guid id, Guid courseId, string title, string videoSrc, Quiz? quiz)
        {
            Id = id;
            CourseId = courseId;
            Title = title;
            VideoSrc = videoSrc;
            Quiz = quiz;
        }

        public Lesson(Guid id, Guid courseId, string title, string videoSrc)
        {
            Id = id;
            CourseId = courseId;
            Title = title;
            VideoSrc = videoSrc;
        }

        public Guid Id { get;  set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string VideoSrc { get; set; }
        public Quiz? Quiz { get; set; }

        public Domain.Lesson ToModel()
        {
            return new Domain.Lesson(Id, Title, Title, Quiz.ToModel());
        }

        
    }
}