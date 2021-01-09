using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using ELearn.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Course = ELearn.Infrastructure.Entity.Models.Course;

namespace ELearn.Infrastructure.Entity.Repositories
{
 
    public class CourseDetailsRepo: ICourseDetailsRepo
    {
        private readonly EntityContext _context;
        
        private readonly AuthContext _authContext;
        
        private CourseOverview WithAuthor(Course model)
        {
            var tmp = model.ToModel();
            var user = _authContext.Users.FirstOrDefault(it => it.Id == model.AuthorId.ToString());
            tmp.AppUser = user?.ToModel() ?? throw new InvalidOperationException("Author cannot be null");
            return tmp;
        }

        public CourseDetailsRepo(EntityContext context, AuthContext authContext)
        {
            _context = context;
            _authContext = authContext;
        }

        public async Task<CourseOverview?> Get(Guid id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(p => p.Id == id);
            

            return WithAuthor(course);
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