﻿using Project.Core.Identity.Roles;
using Project.Core.Identity.Roles.Features.CreateOrUpdateRole;
using Project.Infrastructure.Auth.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Project.Infrastructure.Identity.Roles.Endpoints;

public static class CreateOrUpdateRoleEndpoint
{
    public static RouteHandlerBuilder MapCreateOrUpdateRoleEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/", async (CreateOrUpdateRoleCommand request, IRoleService roleService) =>
        {
            return await roleService.CreateOrUpdateRoleAsync(request);
        })
        .WithName(nameof(CreateOrUpdateRoleEndpoint))
        .WithSummary("Create or update a role")
        .RequirePermission("Permissions.Roles.Create")
        .WithDescription("Create a new role or update an existing role.");
    }
}
