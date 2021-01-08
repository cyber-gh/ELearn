using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using ELearn.Domain;
using ELearn.WebApi.Controllers.CreateCourse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearn.WebApi.Controllers.CreateCourse
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CreateCourseController : Controller
    {

        private readonly ICreateCourseRepo _repo;
        
        
        public CreateCourseController(ICreateCourseRepo repo)
        {
            this._repo = repo;
        }


        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] CreateCourseRequest request)
        {


            if (!TryValidateModel(request, nameof(request)))
            {
                return BadRequest("Invalid Form data");
            }
            
            var model = new CourseOverview(Guid.NewGuid(), request.Title, request.PreviewImageUrl, request.Description,0, request.UserLevel, new AppUser());
            
            
            var data = await _repo.Create(model);
            
            return Ok(data);
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