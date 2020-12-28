using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using ELearn.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELearn.Infrastructure.Entity.Repositories
{
 
    public class CourseDetailsRepo: ICourseDetailsRepo
    {
        private readonly EntityContext _context;

        public CourseDetailsRepo(EntityContext context)
        {
            _context = context;
        }

        public async Task<CourseOverview?> Get(Guid id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(p => p.Id == id);

            return course.ToModel();
        }

        public async Task<IEnumerable<Lesson>> GetLessons(Guid id)
        {
            var lessons = await _context.Lessons.Where(p => p.CourseId == id).ToListAsync();

            return lessons.Select(l => l.ToModel()).ToList();
        }

        public async Task<IEnumerable<Review>> GetReviews(Guid id)
        {
            var reviews = await _context.Reviews.Where(r => r.CourseId == id).ToListAsync();

            return reviews.Select(p => p.ToModel()).ToList();
        }
    }
}