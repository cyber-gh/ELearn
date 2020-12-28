using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using ELearn.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ELearn.WebApi.Controllers.CreateCourse
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateCourseController : Controller
    {

        private readonly ICreateCourseRepo _repo;
        
        
        public CreateCourseController(ICreateCourseRepo repo)
        {
            this._repo = repo;
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddCourse([FromBody] CreateCourseRequest request)
        {


            if (!TryValidateModel(request, nameof(request)))
            {
                return BadRequest("Invalid Form data");
            }
            
            var model = new CourseOverview(Guid.NewGuid(), request.Title, request.PreviewImageUrl, request.Description,
                request.Length, request.UserLevel, null);
            
            
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

            await _repo.AddLesson(request.CourseId, new Lesson(Guid.NewGuid(), request.Title, request.VideoSrc, null));

            return Ok();
        }

        [HttpDelete("lesson")]
        public async Task<IActionResult> RemoveLesson([Required] Guid lessonId)
        {
            await _repo.RemoveLesson(lessonId);

            return Ok();
        }

    }
}