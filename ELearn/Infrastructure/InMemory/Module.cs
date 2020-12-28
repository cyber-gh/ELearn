using Autofac;
using ELearn.Application.Repositories;

namespace ELearn.Infrastructure.InMemory
{
    public class Module: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<CourseListRepo>()
                .As<ICourseListRepoReadOnly>()
                .As<ICourseDetailsRepo>()
                .As<ICreateCourseRepo>()
                .InstancePerLifetimeScope();
        }
    }
}