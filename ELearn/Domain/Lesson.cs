using System;

namespace ELearn.Domain
{
    public sealed class Lesson : IEntity
    {
        public Lesson(Guid id, string title, string videoSrc,int duration, Quiz? quiz)
        {
            Id = id;
            Title = title;
            VideoSrc = videoSrc;
            Duration = duration;
            Quiz = quiz;
        }

        public Guid Id { get; private set; }
        public string Title { get; set; }
        public string VideoSrc { get; set; }
        public int Duration { get; set; }
        public Quiz? Quiz { get; set; }
    }
}