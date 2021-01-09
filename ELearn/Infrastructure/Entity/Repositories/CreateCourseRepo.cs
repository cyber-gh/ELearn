using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using ELearn.Domain;
using ELearn.Infrastructure.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Category = ELearn.Infrastructure.Entity.Models.Category;
using Course = ELearn.Domain.Course;
using Lesson = ELearn.Domain.Lesson;

namespace ELearn.Infrastructure.Entity.Repositories
{
    public class CreateCourseRepo: ICreateCourseRepo
    {
        private readonly EntityContext _context;

        public CreateCourseRepo(EntityContext context)
        {
            _context = context;
        }

        

        public async Task<CourseOverview> Create(CourseOverview overview, Guid authorId)
        {
            var course = new Models.Course(Guid.NewGuid(), overview.Title, overview.PreviewImageUrl, overview.Description, overview.Length, overview.UserLevel, authorId);

            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            
            return course.ToModel();
        }

        public async Task AssignCategory(Guid courseId, Guid categoryId)
        {

            var course = new Models.Course {Id = courseId};
            var category = new Models.Category {Id = categoryId};

            await _context.Courses.AddAsync(course);
            _context.Courses.Attach(course);

            await _context.Categories.AddAsync(category);
            _context.Categories.Attach(category);
            
            course.Categories.Add(category);
            
            
            await _context.SaveChangesAsync();
        }

        public async Task UnassignCategory(Guid courseId, Guid categoryId)
        {
            var course = await _context.Courses
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == courseId);
            var category = await _context.Categories.FirstOrDefaultAsync(p => p.Id == categoryId);

            course.Categories.Remove(category);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Lesson>> AddLessons(Guid courseId, List<Lesson> lessons)
        {
            List<Models.Lesson> l = lessons.Select(p => new Models.Lesson(p.Id, courseId, p.Title, p.VideoSrc, p.Duration, null)).ToList();
            var duration = l.Sum(p => p.Duration);
            var course = await _context.Courses.FirstOrDefaultAsync(p => p.Id == courseId);
            course.Length += duration;
            
            await _context.Lessons.AddRangeAsync(l);
            await _context.SaveChangesAsync();
            return lessons;

        }

        public async Task RemoveLesson(Guid lessonId)
        {
            var lesson = _context.Lessons.FirstOrDefault(p => p.Id == lessonId);
            if (lesson != null)
            {
                var course = await _context.Courses.FirstOrDefaultAsync(p => p.Id == lesson.CourseId);
                course.Length -= lesson.Duration;
                _context.Lessons.Remove(lesson);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Lesson?> UpdateLesson(Guid idx, string newName)
        {
            var lesson = await _context.Lessons.FirstOrDefaultAsync(it => it.Id == idx);
            if (lesson == null) return null;
            lesson.Title = newName;

            await _context.SaveChangesAsync();
            return lesson.ToModel();

        }


        public Task<Course> ModifyCourse(Guid idx, Course newCourse)
        {
            throw new NotImplementedException();
        }
    }
}