using System;
using System.ComponentModel.DataAnnotations;

namespace ELearn.WebApi.Controllers.Categories
{
    public class CreateCategoryRequest
    {
        public CreateCategoryRequest(string name)
        {
            Name = name;
        }

        public CreateCategoryRequest()
        {
        }

        [Required]
        [MinLength(6)]
        public String Name { get; set; }
    }
}