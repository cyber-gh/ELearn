using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ELearn.Domain;

namespace ELearn.Application.Repositories
{
    public interface ICourseListRepoReadOnly
    {
        Task<IEnumerable<CourseOverview>> GetAll();
        Task<IEnumerable<CourseOverview>> SearchCourse(String pattern);
        Task<IEnumerable<CourseOverview>> GetByCategory(Guid categoryId);
        Task<IEnumerable<CourseOverview>> GetByCategory(string name);
        
    }
}