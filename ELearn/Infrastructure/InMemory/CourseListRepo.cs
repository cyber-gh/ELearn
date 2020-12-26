using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using ELearn.Domain;
using StringExtensionMethods;

namespace ELearn.Infrastructure.InMemory
{
    public class CourseListRepo: ICourseListRepoReadOnly, ICourseDetailsRepo, ICreateCourseRepo
    {

        private List<Course> Courses = new List<Course>();

        public CourseListRepo()
        {
            const string link = "https://spin.atomicobject.com/wp-content/uploads/research.jpg";
            Courses.Add(new Course(new CourseOverview(Guid.NewGuid(), "This is a test", link, "Test Description", 120, UserLevel.Beginner )));
            Courses.Add(new Course(new CourseOverview(Guid.NewGuid(), "This is another test", link, "Test Description", 120, UserLevel.Beginner )));
            Courses.Add(new Course(new CourseOverview(Guid.NewGuid(), "Interesting test", link, "Test Description", 120, UserLevel.Beginner )));
            Courses.Add(new Course(new CourseOverview(Guid.NewGuid(), "This is a test", link, "Test Description", 120, UserLevel.Beginner )));
            Courses.Add(new Course(new CourseOverview(Guid.NewGuid(), "This is a test", link, "Test Description", 120, UserLevel.Beginner )));
        }

        public async Task<IEnumerable<CourseOverview>> GetAll()
        {
            return Courses.Select(x => x.Overview);
        }

        public async Task<IEnumerable<CourseOverview>> SearchCourse(string pattern)
        {
            return Courses.OrderBy(p => p.Title.LevenshteinDistance(pattern)).Select(p => p.Overview).Take(5);
        }

        public async Task<IEnumerable<CourseOverview>> GetByCategory(Guid categoryId)
        {
            return new List<CourseOverview>();
        }

        public async Task<CourseOverview> Get(Guid id)
        {
            return Courses.Select(p => p.Overview).First(p => p.Id == id);
        }

        public async Task<IEnumerable<Lesson>> GetLessons(Guid id)
        {
            return Courses.FirstOrDefault(p => p.Id == id)?.Lessons ?? Array.Empty<Lesson>();
        }

        public async Task<IEnumerable<Review>> GetReviews(Guid id)
        {
            return Courses.FirstOrDefault(p => p.Id == id)?.Reviews ?? Array.Empty<Review>();
        }

        public async Task<CourseOverview> Create(CourseOverview overview)
        {
            Courses.Add(new Course(overview, new List<Lesson>(), new List<Review>()));
            return overview;
        }

        public void AddLessons(List<Lesson> lessons)
        {
            throw new NotImplementedException();
        }

        public Task<Course> ModifyCourse(Guid idx, Course newCourse)
        {
            throw new NotImplementedException();
        }
    }
}