using System;
using Autofac;
using ELearn.Application.Repositories;
using ELearn.Infrastructure.Entity.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ELearn.Infrastructure.Entity
{
    public class Module: Autofac.Module
    {
        public string ConnectionString = "DataSource=app.db;Cache=Shared";

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
            
        }
    }
}