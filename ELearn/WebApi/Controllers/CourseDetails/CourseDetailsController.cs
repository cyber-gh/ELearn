using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearn.WebApi.Controllers.CourseDetails
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class CourseDetailsController : Controller
    {

        private readonly ICourseDetailsRepo _repo;
        private readonly IAnalyticsRepo _analytics;

        public CourseDetailsController(ICourseDetailsRepo repo, IAnalyticsRepo analytics)
        {
            _repo = repo;
            _analytics = analytics;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourse([Required] Guid id)
        {
            var course = await _repo.GetDetails(id);
            var userId = Guid.Parse(HttpContext.User.Identity.GetSubjectId());

            await _analytics.AddUserCourseVisited(userId, id);

            
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