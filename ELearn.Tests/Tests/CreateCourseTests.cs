using System;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using ELearn.Domain;
using ELearn.WebApi.Controllers.CreateCourse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Moq;
using Tests.Generic;
using Xunit;

namespace Tests
{
    public class CreateCourseTests: GenericTests
    {

        private static Mock<IObjectModelValidator> validator()
        {
            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(o => o.Validate(It.IsAny<ActionContext>(), 
                It.IsAny<ValidationStateDictionary>(), 
                It.IsAny<string>(), 
                It.IsAny<Object>()));
            return objectValidator;
        }
        
        [Fact]
        public async Task CreatesNewCourseSavesToDb()
        {
            var mockRepo = new Mock<ICreateCourseRepo>();
            var request = new CreateCourseRequest("This is a course", "https://www.google.com", "Most awesome course ever", 120, UserLevel.Beginner);
            var model = new CourseOverview(Guid.Empty, "", "", "", 2, UserLevel.Beginner, null);

            mockRepo.Setup(p => p.Create(It.IsAny<CourseOverview>())).ReturnsAsync(model);


            var controller = new CreateCourseController(mockRepo.Object);
            controller.ObjectValidator = validator().Object;
            var result = await controller.AddCourse(request);
            
            mockRepo.Verify(m => m.Create(It.IsAny<CourseOverview>()), Times.Once);

        }

        [Fact]
        public async Task CanAddNewCourseLesson()
        {
            var mockRepo = new Mock<ICreateCourseRepo>();
            var request = new AddLessonRequest(Guid.Empty, "First Lesson", "https://www.google.com");

            var controller = new CreateCourseController(mockRepo.Object);
            controller.ObjectValidator = validator().Object;

            var result = await controller.AddLesson(request);
            
            mockRepo.Verify(m => m.AddLesson(It.IsAny<Guid>(), It.IsAny<Lesson>()), Times.Once);

        }

        [Fact]
        public async Task CanRemoveCourseLesson()
        {
            var mockRepo = new Mock<ICreateCourseRepo>();

            var controller = new CreateCourseController(mockRepo.Object);
            controller.ObjectValidator = validator().Object;

            var id = Guid.NewGuid();
            var result = await controller.RemoveLesson(id);
            
            mockRepo.Verify(m => m.RemoveLesson(id), Times.Once);

        }
    }
}