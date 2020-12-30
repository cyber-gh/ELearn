using System;
using ELearn.Infrastructure.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests.Generic
{
    public abstract class GenericTests
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private SqliteConnection _connection;


        protected EntityContext GetContext()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<EntityContext>()
                .UseSqlite(_connection)
                .Options;
            var dbContext = new EntityContext(options);
            dbContext.Database.EnsureCreated();

            return dbContext;
        }


        public void Dispose()
        {
            _connection.Close();
        }
        
        protected static Mock<IObjectModelValidator> validator()
        {
            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(o => o.Validate(It.IsAny<ActionContext>(), 
                It.IsAny<ValidationStateDictionary>(), 
                It.IsAny<string>(), 
                It.IsAny<Object>()));
            return objectValidator;
        }
    }
}