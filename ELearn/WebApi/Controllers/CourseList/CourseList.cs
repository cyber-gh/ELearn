using System.Threading.Tasks;
using ELearn.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ELearn.Controllers.CourseList
{
    [Route("api/[controller]")]
    public class CourseListController : Controller
    {

        private readonly ICourseListRepoReadOnly _repo;

        public CourseListController(ICourseListRepoReadOnly repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var courses = await _repo.GetAll();
            return new ObjectResult(courses);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string pattern)
        {
            // return new ObjectResult(pattern);
            var courses = await _repo.SearchCourse(pattern);
            return new ObjectResult(courses);
        }
    }
}