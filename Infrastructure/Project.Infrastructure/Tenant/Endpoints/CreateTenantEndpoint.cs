﻿using Project.Core.Tenant.Features.CreateTenant;
using Project.Infrastructure.Auth.Policy;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Project.Infrastructure.Tenant.Endpoints;
public static class CreateTenantEndpoint
{
    internal static RouteHandlerBuilder MapRegisterTenantEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/", (CreateTenantCommand request, ISender mediator) => mediator.Send(request))
                                .WithName(nameof(CreateTenantEndpoint))
                                .WithSummary("creates a tenant")
                                .RequirePermission("Permissions.Tenants.Create")
                                .WithDescription("creates a tenant");
    }
}
