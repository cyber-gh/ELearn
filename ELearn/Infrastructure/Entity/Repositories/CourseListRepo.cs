using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using ELearn.Domain;
using Microsoft.EntityFrameworkCore;
using StringExtensionMethods;
using Course = ELearn.Infrastructure.Entity.Models.Course;

namespace ELearn.Infrastructure.Entity.Repositories
{
    public class CourseListRepo: ICourseListRepoReadOnly
    {

        private readonly EntityContext _context;
        private readonly AuthContext _authContext;

        public CourseListRepo(EntityContext context, AuthContext authContext)
        {
            _context = context;
            _authContext = authContext;
        }

        private CourseOverview WithAuthor(Course model)
        {
            var tmp = model.ToModel();
            var user = _authContext.Users.FirstOrDefault(it => it.Id == model.AuthorId.ToString());
            tmp.AppUser = user?.ToModel() ?? throw new InvalidOperationException("Author cannot be null");
            return tmp;
        }

        public async Task<IEnumerable<CourseOverview>> GetAll()
        {
            var courses = await _context.Courses
                .Include(c => c.Categories)
                .ToListAsync();
            
            return courses.Select(WithAuthor);;
        }

        public async Task<IEnumerable<CourseOverview>> SearchCourse(string pattern)
        {
            var courses = await _context.Courses
                .Include(c => c.Categories)
                .ToListAsync();
            
            var filtered = courses.OrderBy(p => p.Title.LevenshteinDistance(pattern))
                .Where(p => (p.Title.LevenshteinDistance(pattern) < Math.Max(p.Title.Length, pattern.Length) * 0.5 ) || (p.Title.ToLower().Contains(pattern.ToLower())) ).ToList()
                .Select(p => p);

            return filtered.Select(WithAuthor).ToList();
        }

        public async Task<IEnumerable<CourseOverview>> GetByCategory(Guid categoryId)
        {
            var courses = await _context.Courses
                .Include(c => c.Categories)
                .Where(p => p.Categories.Any(c => c.Id == categoryId)).ToListAsync();
            
            return courses.Select(WithAuthor).ToList();
        }

        public async Task<IEnumerable<CourseOverview>> GetByCategory(string name)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());

            if (category != null)
            {
                return _context.Courses
                    .Include(c => c.Categories)
                    .Where(p => p.Categories.Any(c => c.Id == category.Id))
                    .Select(WithAuthor);
            }
            else
            {
                return new List<CourseOverview>();
            }
        }
    }
}