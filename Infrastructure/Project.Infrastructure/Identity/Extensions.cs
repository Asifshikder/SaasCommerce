using Project.Core.Audit;
using Project.Core.Identity.Roles;
using Project.Core.Identity.Tokens;
using Project.Core.Identity.Users.Abstractions;
using Project.Core.Persistence;
using Project.Infrastructure.Auth;
using Project.Infrastructure.Identity.Audit;
using Project.Infrastructure.Identity.Persistence;
using Project.Infrastructure.Identity.Roles;
using Project.Infrastructure.Identity.Roles.Endpoints;
using Project.Infrastructure.Identity.Tokens;
using Project.Infrastructure.Identity.Tokens.Endpoints;
using Project.Infrastructure.Identity.Users;
using Project.Infrastructure.Identity.Users.Endpoints;
using Project.Infrastructure.Identity.Users.Services;
using Project.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using IdentityConstants = Project.Shared.Authorization.IdentityConstants;

namespace Project.Infrastructure.Identity;
internal static class Extensions
{
    internal static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddScoped<CurrentUserMiddleware>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped(sp => (ICurrentUserInitializer)sp.GetRequiredService<ICurrentUser>());
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IAuditService, AuditService>();
        services.BindDbContext<IdentityDbContext>();
        services.AddScoped<IDbInitializer, IdentityDbInitializer>();
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
           {
               options.Password.RequiredLength = IdentityConstants.PasswordLength;
               options.Password.RequireDigit = false;
               options.Password.RequireLowercase = false;
               options.Password.RequireNonAlphanumeric = false;
               options.Password.RequireUppercase = false;
               options.User.RequireUniqueEmail = true;
           })
           .AddEntityFrameworkStores<IdentityDbContext>()
           .AddDefaultTokenProviders();
        return services;
    }

    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        var users = app.MapGroup("api/users").WithTags("users");
        users.MapUserEndpoints();

        var tokens = app.MapGroup("api/token").WithTags("token");
        tokens.MapTokenEndpoints();

        var roles = app.MapGroup("api/roles").WithTags("roles");
        roles.MapRoleEndpoints();

        return app;
    }
}
