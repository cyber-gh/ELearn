using System;
using ELearn.Domain;

namespace ELearn.Infrastructure.Entity.Models
{
    public sealed class Lesson 
    {
        public Lesson(Guid id, Guid courseId, string title, string videoSrc, int duration, Quiz? quiz)
        {
            Id = id;
            CourseId = courseId;
            Title = title;
            VideoSrc = videoSrc;
            Quiz = quiz;
            Duration = duration;
        }

        public Lesson(Guid id, Guid courseId, string title, string videoSrc, int duration)
        {
            Id = id;
            CourseId = courseId;
            Title = title;
            VideoSrc = videoSrc;
            Duration = duration;
        }

        public Guid Id { get;  set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string VideoSrc { get; set; }
        public int Duration { get; set; }
        public Quiz? Quiz { get; set; }

        public Domain.Lesson ToModel()
        {
            return new Domain.Lesson(Id, Title, VideoSrc,  Duration, Quiz?.ToModel());
        }

        
    }
}