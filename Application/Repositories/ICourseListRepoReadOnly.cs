using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ELearn.Domain;

namespace ELearn.Application.Repositories
{
    public interface ICourseListRepoReadOnly
    {
        Task<List<Course>> GetAll();
        Task<List<Course>> SearchCourse(String pattern);
        Task<List<Course>> GetByCategory(Guid categoryId);
    }
}