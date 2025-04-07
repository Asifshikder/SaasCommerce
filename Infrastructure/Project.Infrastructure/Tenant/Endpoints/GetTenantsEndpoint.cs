using Project.Core.Tenant.Features.GetTenants;
using Project.Infrastructure.Auth.Policy;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Project.Infrastructure.Tenant.Endpoints;
public static class GetTenantsEndpoint
{
    internal static RouteHandlerBuilder MapGetTenantsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/", (ISender mediator) => mediator.Send(new GetTenantsQuery()))
                                .WithName(nameof(GetTenantsEndpoint))
                                .WithSummary("get tenants")
                                .RequirePermission("Permissions.Tenants.View")
                                .WithDescription("get tenants");
    }
}
