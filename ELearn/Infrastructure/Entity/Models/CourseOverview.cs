using System;

namespace ELearn.Infrastructure.Entity.Models
{
    public sealed class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string PreviewImageUrl { get; set; }
        public String Description { get; set; }
        public int Length { get; set; } //seconds total
        public UserLevel UserLevel { get; set; }
        public Guid CategoryId { get; set; }
    }
}