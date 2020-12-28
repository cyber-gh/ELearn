using System;
using ELearn.Domain;

namespace ELearn.Infrastructure.Entity.Models
{
    public sealed class Lesson 
    {
        public Guid Id { get;  set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string VideoSrc { get; set; }
        public Quiz? Quiz { get; set; }
    }
}