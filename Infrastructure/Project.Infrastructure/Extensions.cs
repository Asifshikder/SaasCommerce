using Asp.Versioning.Conventions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Project.Core.Origin;
using Project.Infrastructure.Auth;
using Project.Infrastructure.Auth.Jwt;
using Project.Infrastructure.Behaviours;
using Project.Infrastructure.Caching;
using Project.Infrastructure.Cors;
using Project.Infrastructure.Exceptions;
using Project.Infrastructure.Identity;
using Project.Infrastructure.Jobs;
using Project.Infrastructure.Logging.Serilog;
using Project.Infrastructure.Mail;
using Project.Infrastructure.OpenApi;
using Project.Infrastructure.Persistence;
using Project.Infrastructure.RateLimit;
using Project.Infrastructure.SecurityHeaders;
using Project.Infrastructure.Storage.Files;
using Project.Infrastructure.Tenant;
using Project.Infrastructure.Tenant.Endpoints;
using System.Reflection;

namespace Project.Infrastructure;

public static class Extensions
{
    public static WebApplicationBuilder ConfigureInfrastructure(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.ConfigureSerilog();
        builder.ConfigureDatabase();
        builder.Services.ConfigureMultitenancy();
        builder.Services.ConfigureIdentity();
        builder.Services.AddCorsPolicy(builder.Configuration);
        builder.Services.ConfigureFileStorage();
        builder.Services.ConfigureJwtAuth();
        builder.Services.ConfigureOpenApi();
        builder.Services.ConfigureJobs(builder.Configuration);
        builder.Services.ConfigureMailing();
        builder.Services.ConfigureCaching(builder.Configuration);
        builder.Services.AddExceptionHandler<CustomExceptionHandler>();
        builder.Services.AddProblemDetails();
        builder.Services.AddHealthChecks();
        builder.Services.AddOptions<OriginOptions>().BindConfiguration(nameof(OriginOptions));

        //builder.Services.BindDbContext<ApplicationDbContext>();
        //builder.Services.AddScoped<IDbInitializer, ApplicationDbContextInitializer>();
        // Define module assemblies
        var assemblies = new Assembly[]
        {
            typeof(Project.Core.Core).Assembly,
            typeof(Infrastructure).Assembly
        };

        // Register validators
        builder.Services.AddValidatorsFromAssemblies(assemblies);

        // Register MediatR
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assemblies);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        //builder.Services.ConfigureRateLimit(builder.Configuration);
        //builder.Services.ConfigureSecurityHeaders(builder.Configuration);

        return builder;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        //app.UseRateLimit();
        //app.UseSecurityHeaders();
        app.UseMultitenancy();
        app.UseExceptionHandler();
        app.UseCorsPolicy();
        app.UseOpenApi();
        app.UseJobDashboard(app.Configuration);
        app.UseRouting();
        app.UseStaticFiles();
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "assets")),
            RequestPath = new PathString("/assets")
        });
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapTenantEndpoints();
        app.MapIdentityEndpoints();

        // Current user middleware
        app.UseMiddleware<CurrentUserMiddleware>();

        // Register API versions
        var versions = app.NewApiVersionSet()
                    .HasApiVersion(1)
                    .HasApiVersion(2)
                    .ReportApiVersions()
                    .Build();

        // Map versioned endpoint
        app.MapGroup("api/v{version:apiVersion}").WithApiVersionSet(versions);

        return app;
    }
}
