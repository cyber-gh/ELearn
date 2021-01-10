using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using ELearn.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Course = ELearn.Infrastructure.Entity.Models.Course;

namespace ELearn.Infrastructure.Entity.Repositories
{
 
    public class CourseDetailsRepo: ICourseDetailsRepo
    {
        private readonly EntityContext _context;
        
        private readonly AuthContext _authContext;
        
        private CourseOverview WithDetails(Course model)
        {
            var tmp = model.ToModel();
            var user = _authContext.Users.FirstOrDefault(it => it.Id == model.AuthorId.ToString());
            tmp.AppUser = user?.ToModel() ?? throw new InvalidOperationException("Author cannot be null");
            var visitors = (_context.UserCourses.Where(p => p.CourseId == model.Id).Distinct().Count());
            tmp.Visitors = visitors;
            return tmp;
        }

        private Domain.Review WithDetails(Models.Review model)
        {
            var dModel = model.ToModel();
            var user = _authContext.Users.FirstOrDefault(it => it.Id == model.UserId.ToString());
            dModel.User = user.ToModel();
            return dModel;
        }

        public Domain.Lesson WithDetails(Models.Lesson model)
        {
            var dModel = model.ToModel();
            
            if (model.QuizId == null || model.QuizId == Guid.Empty) return dModel;
            dModel.Quiz = new Domain.Quiz()
            {
                Id = (Guid) model.QuizId
            };
            dModel.Quiz.Elements = _context.QuizElements.Where(it => it.QuizId == model.QuizId)
                .Select(it => new Domain.QuizElement(it.Id, it.Question, it.Answers, it.CorrectAnswer)).ToList();

            return dModel;
        }

        public CourseDetailsRepo(EntityContext context, AuthContext authContext)
        {
            _context = context;
            _authContext = authContext;
        }

        public async Task<CourseOverview?> Get(Guid id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(p => p.Id == id);
            

            return WithDetails(course);
        }

        public async Task<IEnumerable<Lesson>> GetLessons(Guid id)
        {
            var lessons = await _context.Lessons.Where(p => p.CourseId == id).ToListAsync();

            return lessons.Select(WithDetails).ToList();
        }

        public async Task<IEnumerable<Review>> GetReviews(Guid id)
        {
            var reviews = await _context.Reviews.Where(r => r.CourseId == id) .ToListAsync();

            return reviews.Select(WithDetails).ToList();
        }

        public async Task<Review> AddReview(Review rv, Guid authorId, Guid courseId)
        {
            var model = new ELearn.Infrastructure.Entity.Models.Review()
            {
                Id = rv.Id,
                UserId = authorId,
                CourseId = courseId,
                Title = rv.Title,
                Comment = rv.Comment,
                RecommendFor = rv.RecommendFor,
                CreatedDate = DateTime.Now
            };
            
            var exists = _context.Reviews.Count(it => it.UserId == authorId) >= 1;
            if (exists) throw new InvalidOperationException("User already reviewed this course");
            await _context.Reviews.AddAsync(model);
            await _context.SaveChangesAsync();
            return WithDetails(model);
        }
    }
}