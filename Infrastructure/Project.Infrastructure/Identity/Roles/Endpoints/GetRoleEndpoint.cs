using Project.Core.Identity.Roles;
using Project.Infrastructure.Auth.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Project.Infrastructure.Identity.Roles.Endpoints;

public static class GetRoleByIdEndpoint
{
    public static RouteHandlerBuilder MapGetRoleEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/{id:guid}", async (string id, IRoleService roleService) =>
        {
            return await roleService.GetRoleAsync(id);
        })
        .WithName(nameof(GetRoleByIdEndpoint))
        .WithSummary("Get role details by ID")
        .RequirePermission("Permissions.Roles.View")
        .WithDescription("Retrieve the details of a role by its ID.");
    }
}

