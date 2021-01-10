using System;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using ELearn.Infrastructure.Entity.Models;

namespace ELearn.Infrastructure.Entity.Repositories
{
    public class AnalyticsRepo: IAnalyticsRepo
    {
        
        private readonly EntityContext _context;


        public AnalyticsRepo(EntityContext context)
        {
            _context = context;
        }

        public async Task AddUserCourseVisited(Guid userId, Guid courseId)
        {
            await _context.UserCourses.AddAsync(new UserCourse() {UserId = userId, CourseId = courseId});
            await _context.SaveChangesAsync();
        }
    }
}