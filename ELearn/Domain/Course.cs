using System;
using System.Collections.Generic;

namespace ELearn.Domain
{
    public sealed class Course: IEntity
    {
        public Course(CourseOverview overview, IEnumerable<Lesson> lessons,IEnumerable<Review> reviews)
        {
            Overview = overview;
            Reviews = reviews;
            Lessons = lessons;
        }

        public Course(CourseOverview overview)
        {
            Overview = overview;
            Reviews = new List<Review>();
            Lessons = new List<Lesson>();
        }


        public CourseOverview Overview;

        public Course()
        {
            
        }

        public Guid Id
        {
            get => Overview.Id;
            set => Overview.Id = value;
        }
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }
        
    }
}