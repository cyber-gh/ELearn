using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ELearn.Domain;

namespace ELearn.Application.Repositories
{
    public interface ICreateCourseRepo
    {
        Task<CourseOverview> Create(CourseOverview overview);
        void AddLessons(List<Lesson> lessons);
        Task<Course> ModifyCourse(Guid idx, Course newCourse);
    }
}