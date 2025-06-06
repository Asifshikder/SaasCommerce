using Carter;
using FluentValidation;
using System.Reflection;
using Asp.Versioning.Conventions;
using Project.Application;
using Project.Persistance;

namespace Webapi
{
    public static class Extensions
    {
        public static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            //define module assemblies
            var assemblies = new Assembly[]
            {
            typeof(ApplicationMetaData).Assembly
            };

            //register validators
            builder.Services.AddValidatorsFromAssemblies(assemblies);

            //register mediatr
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assemblies);
            });

            //register module services
            builder.RegisterFeatureServices();
            builder.RegisterPersistenceServices();

            //add carter endpoint modules
            builder.Services.AddCarter(configurator: config =>
            {
                config.WithModule<Project.Application.ServiceCollectionExtensions.Endpoints>();
            });

            return builder;
        }

        public static WebApplication UseModules(this WebApplication app)
        {
            ArgumentNullException.ThrowIfNull(app);

            //register modules
            app.UseApplicationModule();
            app.UsePersistenceModule();

            //register api versions
            var versions = app.NewApiVersionSet()
                        .HasApiVersion(1)
                        .HasApiVersion(2)
                        .ReportApiVersions()
                        .Build();

            //map versioned endpoint
            var endpoints = app.MapGroup("api/v{version:apiVersion}").WithApiVersionSet(versions);

            //use carter
            endpoints.MapCarter();

            return app;
        }
    }
}
