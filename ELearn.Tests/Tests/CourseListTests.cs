using System;
using System.Collections.Generic;
using ELearn.Application.Repositories;
using ELearn.Domain;
using ELearn.Infrastructure.Entity.Repositories;
using ELearn.WebApi.Controllers.CourseList;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Tests.Generic;
using Xunit;

namespace Tests
{
    public class CourseListTests: GenericTests
    {

        private CourseListController GetController()
        {
            var repo = new CourseListRepo(GetContext());
            var controller = new CourseListController(repo);
            controller.ObjectValidator = validator().Object;
            return controller;
        }

        private List<CourseOverview> GetDummyList()
        {
            var list = new List<CourseOverview>();
            const string link = "https://spin.atomicobject.com/wp-content/uploads/research.jpg";
            list.Add(new CourseOverview(Guid.NewGuid(), "This is a test", link, "Test Description", 120, UserLevel.Beginner ));
            list.Add(new CourseOverview(Guid.NewGuid(), "This is another test", link, "Test Description", 120, UserLevel.Beginner ));
            list.Add(new CourseOverview(Guid.NewGuid(), "Interesting test", link, "Test Description", 120, UserLevel.Beginner ));
            list.Add(new CourseOverview(Guid.NewGuid(), "This is a test", link, "Test Description", 120, UserLevel.Beginner ));
            list.Add(new CourseOverview(Guid.NewGuid(), "This is a test", link, "Test Description", 120, UserLevel.Beginner ));
            list.Add(new CourseOverview(Guid.NewGuid(), "Special Feature", link, "Test Description", 120, UserLevel.Beginner ));
            list.Add(new CourseOverview(Guid.NewGuid(), "Azkaban", link, "Test Description", 120, UserLevel.Beginner ));

            return list;
        }
        
        [Fact]
        public async void CourseListController_ReturnsAllCourses()
        {
            var mock = new Mock<ICourseListRepoReadOnly>();
            var list = GetDummyList();
            mock.Setup(p => p.GetAll()).ReturnsAsync(list);

            var controller = new CourseListController(mock.Object);

            var result = await controller.Get();
            
            AssertRequestOk(list, result);


        }

        [Theory]
        [InlineData("special")]
        [InlineData("azkaban")]
        [InlineData("azkban")]
        [InlineData("Azkaban")]
        public async void CourseListSearchReturnsResults(string pattern)
        {
            var list = GetDummyList();
            var context = GetContext();
            var repo = new CourseListRepo(context);
            var controller = new CourseListController(repo);
            controller.ObjectValidator = validator().Object;
            var addRepo = new CreateCourseRepo(context);
            
            foreach (var courseOverview in list)
            {
                await addRepo.Create(courseOverview);
            }

            var result = await controller.Search(pattern);
            var resultList = (result as OkObjectResult)?.Value as List<CourseOverview>;
            
            Assert.True(resultList != null && resultList.Count == 1);

        }

        private void AssertRequestOk<T>(T original, IActionResult result) where T : class
        {
            var okResult = result as ObjectResult;
            Assert.True(okResult != null);
            // Assert.Equal(200, okResult.StatusCode);

            Assert.IsType<T>(okResult.Value);
            var valueT = okResult.Value as T;
            Assert.Equal(original, valueT);
        }
    }
}