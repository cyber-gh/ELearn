using System;
using System.Collections.Generic;
using ELearn.Application.Repositories;
using ELearn.Controllers.CourseList;
using ELearn.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Tests
{
    public class CourseListTests
    {
        [Fact]
        public async void CourseListController_ReturnsAllCourses()
        {
            var mock = new Mock<ICourseListRepoReadOnly>();
            var list = new List<CourseOverview>();
            const string link = "https://spin.atomicobject.com/wp-content/uploads/research.jpg";
            list.Add(new CourseOverview(Guid.NewGuid(), "This is a test", link, "Test Description", 120, UserLevel.Beginner ));
            list.Add(new CourseOverview(Guid.NewGuid(), "This is another test", link, "Test Description", 120, UserLevel.Beginner ));
            list.Add(new CourseOverview(Guid.NewGuid(), "Interesting test", link, "Test Description", 120, UserLevel.Beginner ));
            list.Add(new CourseOverview(Guid.NewGuid(), "This is a test", link, "Test Description", 120, UserLevel.Beginner ));
            list.Add(new CourseOverview(Guid.NewGuid(), "This is a test", link, "Test Description", 120, UserLevel.Beginner ));

            mock.Setup(p => p.GetAll()).ReturnsAsync(list);

            var controller = new CourseListController(mock.Object);

            var result = await controller.Get();
            
            AssertRequestOk(list, result);


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