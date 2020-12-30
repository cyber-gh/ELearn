using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using ELearn.Domain;
using ELearn.Infrastructure.Entity.Repositories;
using ELearn.WebApi.Controllers.Categories;
using Microsoft.AspNetCore.Mvc;
using Tests.Generic;
using Xunit;

namespace Tests
{
    public class CategoriesTests: GenericTests
    {

        private CategoriesController CreateController()
        {
            var repo = new CategoryRepo(GetContext());
            var controller = new CategoriesController(repo);
            controller.ObjectValidator = validator().Object;
            return controller;
        }

        [Theory]
        [InlineData("Category 1", "Cateogry 2")]
        [InlineData("Category 1", "Cateogry 2", "Category 4")]
        [InlineData("Category 1")]
        public async Task CanAddNewCategoriesAndAreSaved(params string[] names)
        {
            var controller = CreateController();
            foreach (var name in names)
            {
                var category = new CreateCategoryRequest(name);
                var result = await controller.Create(category);
                Assert.True(result is OkObjectResult);
            }
            
            var categoriesResult = await controller.GetAll();
            
            Assert.True(categoriesResult is ObjectResult);
            var categories = (categoriesResult as OkObjectResult)?.Value as List<Category>;

            var resultedNames = categories.Select(p => p.Name).ToHashSet();
            var originalNames = names.ToHashSet();
            Assert.Equal(originalNames, resultedNames);

            
        }
        
        [Theory]
        [InlineData("Category 1", "Cateogry 2")]
        [InlineData("Category 1", "Cateogry 2", "Category 4")]
        [InlineData("Category 1")]
        public async Task CandDeleteInsertedCategories(params string[] names)
        {
            var controller = CreateController();
            foreach (var name in names)
            {
                var category = new CreateCategoryRequest(name);
                var result = await controller.Create(category);
                Assert.True(result is OkObjectResult);
            }
            
            var categoriesResult = await controller.GetAll();
            
            Assert.True(categoriesResult is ObjectResult);
            var categories = (categoriesResult as OkObjectResult)?.Value as List<Category>;

            var resultedNames = categories.Select(p => p.Name).ToHashSet();
            var originalNames = names.ToHashSet();
            Assert.Equal(originalNames, resultedNames);
            
            foreach (var category in categories)
            {
                var result = await controller.Delete(category.Id);
                Assert.True(result is OkResult);
            }
            
            var categoriesNewResult = await controller.GetAll();
            
            Assert.True(categoriesNewResult is ObjectResult);
            var categoriesNew = (categoriesNewResult as OkObjectResult)?.Value as List<Category>;
            
            Assert.Empty(categoriesNew);
            
            

            
        }
        
    }
}