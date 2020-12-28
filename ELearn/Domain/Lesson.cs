using System;

namespace ELearn.Domain
{
    public sealed class Lesson : IEntity
    {
        public Lesson(Guid id, string title, string videoSrc, Quiz? quiz)
        {
            Id = id;
            Title = title;
            VideoSrc = videoSrc;
            Quiz = quiz;
        }

        public Guid Id { get; private set; }
        public string Title { get; set; }
        public string VideoSrc { get; set; }
        public Quiz? Quiz { get; set; }
    }
}