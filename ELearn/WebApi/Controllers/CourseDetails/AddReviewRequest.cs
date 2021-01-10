using System;
using System.ComponentModel.DataAnnotations;
using ELearn.Domain;

namespace ELearn.WebApi.Controllers.CourseDetails
{
    public class AddReviewRequest
    {
        [Required]
        public Guid CourseId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public UserLevel RecommendFor { get; set; } // 1 .. 5
    }
}