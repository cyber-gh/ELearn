using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ELearn.Application.Utils;
using ELearn.Domain;

namespace ELearn.WebApi.Controllers.CreateCourse.Models
{
    public class AddQuizRequest
    {
        [Required]
        public Guid LessonId { get; set; }

        [Required]
        [LimitCount(3,5, ErrorMessage = "Number of elements must be between 3 and 5")]
        public List<AddQuizRequestElement> Elements { get; set; }

        public class AddQuizRequestElement
        {
            [Required]
            public string Question { get; set; }
            [Required]
            [LimitCount(3,5, ErrorMessage = "Number of elements must be between 3 and 5")]
            public List<String> Answers { get; set; }
            [Required]
            public String CorrectAnswer { get; set; }
        }
    }
}