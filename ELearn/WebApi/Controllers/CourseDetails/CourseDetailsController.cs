using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using ELearn.Domain;
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
        private readonly IUserRepo _userRepo;

        public CourseDetailsController(ICourseDetailsRepo repo, IAnalyticsRepo analytics, IUserRepo userRepo)
        {
            _repo = repo;
            _analytics = analytics;
            _userRepo = userRepo;
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
            
            foreach (var lesson in lessons)
            {
                if (lesson.Quiz?.Elements == null) continue;
                foreach (var quizElement in lesson.Quiz?.Elements)
                {
                    quizElement.CorrectAnswer = "";
                }
            }

            return Ok(lessons);
        }

        [HttpGet("reviews")]
        public async Task<IActionResult> GetReviews([Required] Guid id)
        {
            var reviews = await _repo.GetReviews(id);

            return Ok(reviews);
        }

        /*
         * Add a review, returns the created review
         */
        [HttpPost("reviews")]
        public async Task<IActionResult> AddReview([FromBody] AddReviewRequest request)
        {
            if (!TryValidateModel(request, nameof(request)))
            {
                return BadRequest("Invalid form data");
            }

            try
            {
                
                var userId = Guid.Parse(HttpContext.User.Identity.GetSubjectId());
                var model = new Review(Guid.NewGuid(), request.Title, request.Comment,
                    request.RecommendFor);
                var result = await _repo.AddReview(model, userId, request.CourseId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("quiz/submit")]
        public async Task<IActionResult> SubmitQuiz([FromBody] SubmitQuizRequest request)
        {
            if (!TryValidateModel(request, nameof(request)))
            {
                return BadRequest("Invalid form data");
            }

            var lessons = await _repo.GetLessons(request.CourseId);
            var lesson = lessons.FirstOrDefault(it => it.Id == request.LessonId);

            if (lesson == null)
            {
                return BadRequest("No such quiz or lesson");
            }

            if (request.Answers.Count() != lesson.Quiz.Elements.Count())
            {
                return BadRequest("Number of questions doesn't is wrong");
            }

            var answers = new List<QuizResult.QuizResultElement>();
            foreach (var requestAnswer in request.Answers)
            {
                var correctAnswer = lesson.Quiz?.Elements.FirstOrDefault(it => it.Question == requestAnswer.Question)?.CorrectAnswer;
                if (correctAnswer == null) continue;
                answers.Add(new QuizResult.QuizResultElement()
                {
                    Answer = requestAnswer.Answer,
                    CorrectAnswer = correctAnswer,
                    Question = requestAnswer.Question
                });
                
            }
            
            var userId = Guid.Parse(HttpContext.User.Identity.GetSubjectId());
            var user = await _userRepo.GetUser(userId);
            var result = new QuizResult()
            {
                Id = Guid.NewGuid(),
                User = user,
                Elements = answers

            };
            return Ok(result);
        }
    }
}