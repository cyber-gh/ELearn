using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ELearn.Domain;

namespace ELearn.Application.Repositories
{
    public interface ICreateCourseRepo
    {
        Task<CourseOverview> Create(CourseOverview overview);

        Task AssignCategory(Guid courseId, Guid categoryId);
        Task UnassignCategory(Guid courseId, Guid categoryId);
        async Task AddLesson(Guid courseId, Lesson lesson)
        {
            await AddLessons(courseId, new List<Lesson> {lesson});

        }
        Task AddLessons(Guid courseId, List<Lesson> lessons);
        Task RemoveLesson(Guid lessonId);
        Task<Course> ModifyCourse(Guid idx, Course newCourse);
        
        
    }
}