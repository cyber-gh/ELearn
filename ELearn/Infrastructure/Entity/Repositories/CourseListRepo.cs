using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using ELearn.Domain;
using Microsoft.EntityFrameworkCore;
using StringExtensionMethods;

namespace ELearn.Infrastructure.Entity.Repositories
{
    public class CourseListRepo: ICourseListRepoReadOnly
    {

        private readonly EntityContext _context;

        public CourseListRepo(EntityContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseOverview>> GetAll()
        {
            var courses = await _context.Courses.ToListAsync();
            
            return courses.Select(e => e.ToModel());;
        }

        public async Task<IEnumerable<CourseOverview>> SearchCourse(string pattern)
        {
            var courses = await _context.Courses.ToListAsync();
            
            var filtered = courses.OrderBy(p => p.Title.LevenshteinDistance(pattern))
                .Where(p => (p.Title.LevenshteinDistance(pattern) < Math.Max(p.Title.Length, pattern.Length) * 0.5 ) || (p.Title.ToLower().Contains(pattern.ToLower())) ).ToList()
                .Select(p => p);

            return filtered.Select(e => e.ToModel());
        }

        public async Task<IEnumerable<CourseOverview>> GetByCategory(Guid categoryId)
        {
            var courses = await _context.Courses.Where(p => p.Category.Id == categoryId).ToListAsync();

            return courses.Select(e => e.ToModel());
        }
    }
}