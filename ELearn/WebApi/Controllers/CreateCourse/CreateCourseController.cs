using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using ELearn.Domain;
using ELearn.Infrastructure.Entity.Models;
using ELearn.WebApi.Controllers.CreateCourse.Models;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Lesson = ELearn.Domain.Lesson;

namespace ELearn.WebApi.Controllers.CreateCourse
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CreateCourseController : Controller
    {

        private readonly ICreateCourseRepo _repo;
        private readonly UserManager<ApplicationUser> _userManager;
        
        
        public CreateCourseController(ICreateCourseRepo repo, UserManager<ApplicationUser> userManager)
        {
            this._repo = repo;
            _userManager = userManager;
        }

        private async Task<Guid> GetUserId()
        {
            ClaimsPrincipal currentUser = this.User;
            var name = currentUser.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var user = await _userManager.FindByNameAsync(name);
            var id = user.Id;
            if (id == null)
            {
                throw new InvalidOperationException("Current user not set");
            }
            return Guid.Parse(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] CreateCourseRequest request)
        {


            if (!TryValidateModel(request, nameof(request)))
            {
                return BadRequest("Invalid Form data");
            }

            try
            {
                var id = Guid.Parse(HttpContext.User.Identity.GetSubjectId());
                var model = new CourseOverview(Guid.NewGuid(), request.Title, request.PreviewImageUrl, request.Description,0, request.UserLevel);
                
                var data = await _repo.Create(model, id);
            
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("lesson")]
        public async Task<IActionResult> AddLesson([FromBody] AddLessonRequest request)
        {
            if (!TryValidateModel(request, nameof(request)))
            {
                return BadRequest("Invalid Form data");
            }

            var lesson = new Lesson(Guid.NewGuid(), request.Title, request.VideoSrc, request.Duration, null);
            await _repo.AddLesson(request.CourseId, lesson);

            return Ok(lesson);
        }
        
        [HttpPut("lesson")]
        public async Task<IActionResult> UpdateLesson([FromBody] UpdateLessonRequest request)
        {
            if (!TryValidateModel(request, nameof(request)))
            {
                return BadRequest("Invalid Form data");
            }

            var lesson = await _repo.UpdateLesson(request.LessonId, request.Title);

            return Ok(lesson);
        }

        [HttpDelete("lesson")]
        public async Task<IActionResult> RemoveLesson([Required] Guid lessonId)
        {
            await _repo.RemoveLesson(lessonId);

            return Ok();
        }

        [HttpPost("category")]
        public async Task<IActionResult> AssignCategory([FromBody] AssignCategoryRequest request)
        {

            await _repo.AssignCategory(request.CourseId, request.CategoryId);

            return Ok();
        }
        
        [HttpDelete("category")]
        public async Task<IActionResult> UnassignCategory([FromBody] AssignCategoryRequest request)
        {

            await _repo.UnassignCategory(request.CourseId, request.CategoryId);

            return Ok();
        }

    }
}