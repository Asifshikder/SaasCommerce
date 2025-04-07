using Project.Core.Exceptions;
using Project.Core.Jobs;
using Project.Core.Persistence;
using Project.Infrastructure.Persistence;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Project.Infrastructure.Jobs;

internal static class Extensions
{
    internal static IServiceCollection ConfigureJobs(this IServiceCollection services, IConfiguration configuration)
    {
        var dbOptions = configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>() ??
            throw new AppException("database options cannot be null");

        services.AddHangfireServer(o =>
        {
            o.HeartbeatInterval = TimeSpan.FromSeconds(30);
            o.Queues = new string[] { "default", "email" };
            o.WorkerCount = 5;
            o.SchedulePollingInterval = TimeSpan.FromSeconds(30);
        });

        services.AddHangfire((provider, config) =>
        {

            config.UseSqlServerStorage(dbOptions.ConnectionString);


            config.UseFilter(new ApplicationJobFilter(provider));
            config.UseFilter(new LogJobFilter());
        });

        services.AddTransient<IJobService, HangfireService>();
        return services;
    }

    internal static IApplicationBuilder UseJobDashboard(this IApplicationBuilder app, IConfiguration config)
    {
        var hangfireOptions = config.GetSection(nameof(HangfireOptions)).Get<HangfireOptions>() ?? new HangfireOptions();
        var dashboardOptions = new DashboardOptions();
        dashboardOptions.AppPath = "https://fullstackhero.net/";
        dashboardOptions.Authorization = new[]
        {
           new HangfireCustomBasicAuthenticationFilter
           {
                User = hangfireOptions.UserName!,
                Pass = hangfireOptions.Password!
           }
        };

        return app.UseHangfireDashboard(hangfireOptions.Route, dashboardOptions);
    }
}
