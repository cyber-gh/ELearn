using System;
using System.ComponentModel.DataAnnotations;

namespace ELearn.WebApi.Controllers.CreateCourse
{
    public class AssignCategoryRequest
    {
        [Required]
        public Guid CourseId { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
    }
}