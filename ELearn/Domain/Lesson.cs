using System;

namespace ELearn.Domain
{
    public sealed class Lesson : IEntity
    {
        public Guid Id { get; private set; }
        public string Title { get; set; }
        public string VideoSrc { get; set; }
        public Quiz? Quiz { get; set; }
    }
}