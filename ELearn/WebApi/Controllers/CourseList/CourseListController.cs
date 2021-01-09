using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using ELearn.Infrastructure.Entity.Models;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ELearn.WebApi.Controllers.CourseList
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseListController : Controller
    {

        private readonly ICourseListRepoReadOnly _repo;
        private readonly UserManager<ApplicationUser> _userManager;

        public CourseListController(ICourseListRepoReadOnly repo, UserManager<ApplicationUser> userManager)
        {
            this._repo = repo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var courses = await _repo.GetAll();
            return Ok(courses);
        }

        [HttpGet("category")]
        public async Task<IActionResult> GetByCategory([Required] string name)
        {
            var courses = await _repo.GetByCategory(name);

            return Ok(courses);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string pattern)
        {
            // return new ObjectResult(pattern);
            var courses = await _repo.SearchCourse(pattern);
            return Ok(courses);
        }

        [HttpGet("my-classes")]
        [Authorize]
        public async Task<IActionResult> GetMyClasses()
        {
            var userId = Guid.Parse(HttpContext.User.Identity.GetSubjectId());
            var courses = (await _repo.GetAll()).Where(p => p.AppUser.Id == userId);

            return Ok(courses);
        }
    }
}