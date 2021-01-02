using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearn.Domain;

namespace ELearn.Application.Repositories
{
    public interface ICreateCourseRepo
    {
        Task<CourseOverview> Create(CourseOverview overview);

        Task AssignCategory(Guid courseId, Guid categoryId);
        Task UnassignCategory(Guid courseId, Guid categoryId);
        async Task<Lesson> AddLesson(Guid courseId, Lesson lesson)
        {
            var ans = await AddLessons(courseId, new List<Lesson> {lesson});
            return ans.First();
        }
        Task<IEnumerable<Lesson>> AddLessons(Guid courseId, List<Lesson> lessons);
        Task RemoveLesson(Guid lessonId);
        Task<Lesson?> UpdateLesson(Guid idx, string newName);
        Task<Course> ModifyCourse(Guid idx, Course newCourse);
        
        
    }
}