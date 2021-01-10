using System;
using System.Threading.Tasks;

namespace ELearn.Application.Repositories
{
    public interface IAnalyticsRepo
    {
        Task AddUserCourseVisited(Guid userId, Guid courseId);
    }
}