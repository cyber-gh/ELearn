using System;
using System.ComponentModel.DataAnnotations;
using ELearn.Domain;

namespace ELearn.WebApi.Controllers.CreateCourse
{
    public class CreateCourseRequest
    {
        public CreateCourseRequest(string title, string previewImageUrl, string description, int length, UserLevel userLevel)
        {
            Title = title;
            PreviewImageUrl = previewImageUrl;
            Description = description;
            Length = length;
            UserLevel = userLevel;
        }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Title { get; set; }
        [Required]
        [Url]
        public string PreviewImageUrl { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 12)]
        public String Description { get; set; }
        [Required]
        [Range(1, 100000)]
        public int Length { get; set; } //seconds total
        [Required]
        public UserLevel UserLevel { get; set; }
        
    }
}