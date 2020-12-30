using System.Threading.Tasks;
using ELearn.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ELearn.WebApi.Controllers.CourseList
{
    [Route("api/[controller]")]
    [ApiController]
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
            return Ok(courses);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string pattern)
        {
            // return new ObjectResult(pattern);
            var courses = await _repo.SearchCourse(pattern);
            return Ok(courses);
        }
    }
}