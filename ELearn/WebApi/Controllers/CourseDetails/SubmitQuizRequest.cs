using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ELearn.WebApi.Controllers.CreateCourse.Models;

namespace ELearn.WebApi.Controllers.CourseDetails
{
    public class SubmitQuizRequest
    {
        [Required]
        public Guid LessonId { get; set; }
        [Required]
        public Guid CourseId { get; set; }
        
        [Required]
        public List<SubmitQuizRequestElement> Answers { get; set; }
        
        public class SubmitQuizRequestElement
        {
            [Required]
            public string Question { get; set; }
            
            [Required]
            public string Answer { get; set; }
        }
    }
}