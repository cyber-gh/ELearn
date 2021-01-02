using System;
using System.ComponentModel.DataAnnotations;

namespace ELearn.WebApi.Controllers.CreateCourse.Models
{
    public class UpdateLessonRequest
    {
        [Required]
        public Guid LessonId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Title { get; set; }

    }
}