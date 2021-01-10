using System;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using ELearn.Infrastructure.Entity.Models;
using Microsoft.EntityFrameworkCore;

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
            var relation = await _context.UserCourses.FirstOrDefaultAsync(p => p.CourseId == courseId && p.UserId == userId);
            if (relation != null)
            {
                return;
            }
            await _context.UserCourses.AddAsync(new UserCourse() {UserId = userId, CourseId = courseId});
            await _context.SaveChangesAsync();
        }
    }
}