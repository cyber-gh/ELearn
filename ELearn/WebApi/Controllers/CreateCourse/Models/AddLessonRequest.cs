using System;
using System.ComponentModel.DataAnnotations;

namespace ELearn.WebApi.Controllers.CreateCourse.Models
{
    public class AddLessonRequest
    {
        public AddLessonRequest(Guid courseId, string title, string videoSrc)
        {
            CourseId = courseId;
            Title = title;
            VideoSrc = videoSrc;
        }

        public AddLessonRequest()
        {
        }

        [Required]
        public Guid CourseId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Title { get; set; }
        [Required]
        [Range(0, 100000000)]
        public int Duration { get; set; }
        [Required]
        [Url]
        public string VideoSrc { get; set; }
    }
}