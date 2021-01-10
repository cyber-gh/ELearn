using System;
using System.ComponentModel.DataAnnotations;

namespace ELearn.Infrastructure.Entity.Models
{
    public class UserCourse
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
    }
}