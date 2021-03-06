using System;
using Autofac;
using ELearn.Application.Repositories;
using ELearn.Infrastructure.Entity.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ELearn.Infrastructure.Entity
{
    public class EntityModule: Autofac.Module
    {
        // public string ConnectionString = "DataSource=app.db;Cache=Shared";
        public EntityModule(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlite(ConnectionString);
            optionsBuilder.EnableSensitiveDataLogging(true);

            builder.RegisterType<EntityContext>()
                .WithParameter(new TypedParameter(typeof(DbContextOptions), optionsBuilder.Options))
                .InstancePerLifetimeScope();
            
            builder.RegisterType<CourseDetailsRepo>()
                .As<ICourseDetailsRepo>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<CourseListRepo>()
                .As<ICourseListRepoReadOnly>()
                .InstancePerLifetimeScope();
            
            
            builder.RegisterType<CreateCourseRepo>()
                .As<ICreateCourseRepo>()
                .InstancePerLifetimeScope();
            
            
            builder.RegisterType<CategoryRepo>()
                .As<ICategoriesRepo>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AnalyticsRepo>()
                .As<IAnalyticsRepo>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserRepo>()
                .As<IUserRepo>()
                .InstancePerLifetimeScope();

        }
    }
}