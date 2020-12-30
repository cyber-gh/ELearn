using System;
using System.Collections;
using System.Collections.Generic;

namespace ELearn.Infrastructure.Entity.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public String Name { get; set; }

        public ICollection<Course> Courses { get; set; }
        public Category()
        {
            Courses = new HashSet<Course>();
        }

        public Category(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Domain.Category ToModel()
        {
            return new Domain.Category(Id, Name);
        }
        
        
    }
}