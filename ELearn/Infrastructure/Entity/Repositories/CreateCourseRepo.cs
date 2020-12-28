using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using ELearn.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ELearn.Infrastructure.Entity.Repositories
{
    public class CreateCourseRepo: ICreateCourseRepo
    {
        private readonly EntityContext _context;

        public CreateCourseRepo(EntityContext context)
        {
            _context = context;
        }

        

        public async Task<CourseOverview> Create(CourseOverview overview)
        {
            var course = new Models.Course(Guid.NewGuid(), overview.Title, overview.PreviewImageUrl, overview.Description, overview.Length, overview.UserLevel, null);

            await _context.Courses.AddAsync(course);
            _context.SaveChanges();

            return course.ToModel();
        }

        public async Task AddLessons(Guid courseId, List<Lesson> lessons)
        {
            List<Models.Lesson> l = lessons.Select(p => new Models.Lesson(p.Id, courseId, p.Title, p.VideoSrc, null)).ToList();

            await _context.Lessons.AddRangeAsync(l);
            _context.SaveChanges();

        }

        public async Task RemoveLesson(Guid lessonId)
        {
            var lesson = _context.Lessons.FirstOrDefault(p => p.Id == lessonId);
            if (lesson != null)
            {
                _context.Lessons.Remove(lesson);
            }

            await _context.SaveChangesAsync();
        }


        public Task<Course> ModifyCourse(Guid idx, Course newCourse)
        {
            throw new NotImplementedException();
        }
    }
}