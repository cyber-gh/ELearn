using System;
using Autofac;
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
            
        }
    }
}