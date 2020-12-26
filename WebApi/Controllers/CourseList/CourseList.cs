using System.Threading.Tasks;
using ELearn.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ELearn.Controllers.CourseList
{
    [Route("api/[controller]")]
    public class CourseListController : Controller
    {

        private readonly ICourseListRepoReadOnly repo;

        public CourseListController(ICourseListRepoReadOnly repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Get()
        {
            var courses = await repo.GetAll();
            return new ObjectResult(courses);
        }
    }
}