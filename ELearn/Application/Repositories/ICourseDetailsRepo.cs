using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ELearn.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ELearn.Application.Repositories
{
    public interface ICourseDetailsRepo
    {
        Task<CourseOverview?> Get(Guid id);
        Task<IEnumerable<Lesson>> GetLessons(Guid id);
        Task<IEnumerable<Review>> GetReviews(Guid id);
        Task<Review> AddReview(Review rv, Guid authorId, Guid courseId);

        async Task<Course> GetDetails(Guid id)
        {
            var overview = Get(id);
            var lessons = GetLessons(id);
            var reviews = GetReviews(id);

            await Task.WhenAll(overview, lessons, reviews);

            return new Course(overview.Result, lessons.Result, reviews.Result);
        }
        

    }
}