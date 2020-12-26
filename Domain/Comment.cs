using System;

namespace ELearn.Domain
{
    public sealed class Comment: IEntity
    {
        public Guid Id { get; private set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }
        
    }
}