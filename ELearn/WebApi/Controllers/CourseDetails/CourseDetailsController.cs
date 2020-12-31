using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ELearn.WebApi.Controllers.CourseDetails
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CourseDetailsController : Controller
    {

        private readonly ICourseDetailsRepo _repo;

        public CourseDetailsController(ICourseDetailsRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourse([Required] Guid id)
        {
            var course = await _repo.GetDetails(id);


            return Ok(course);
        }

        [HttpGet("lessons")]
        public async Task<IActionResult> GetLessons([Required] Guid id)
        {
            
            var lessons = await _repo.GetLessons(id);

            return Ok(lessons);
        }

        [HttpGet("reviews")]
        public async Task<IActionResult> GetReviews([Required] Guid id)
        {
            var reviews = await _repo.GetReviews(id);

            return Ok(reviews);
        }
    }
}