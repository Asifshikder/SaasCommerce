﻿using Project.Core.Auth.Jwt;
using Project.Infrastructure.Auth.Policy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Project.Infrastructure.Auth.Jwt;
internal static class Extensions
{
    internal static IServiceCollection ConfigureJwtAuth(this IServiceCollection services)
    {
        services.AddOptions<JwtOptions>()
            .BindConfiguration(nameof(JwtOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
        services
            .AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, null!);

        services.AddAuthorizationBuilder().AddRequiredPermissionPolicy();
        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.GetPolicy(RequiredPermissionDefaults.PolicyName);
        });
        return services;
    }
}
