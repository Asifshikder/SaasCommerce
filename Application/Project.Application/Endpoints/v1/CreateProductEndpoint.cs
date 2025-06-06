﻿using Project.Infrastructure.Auth.Policy;
using Project.Application.Products.Create.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Project.Infrastructure.Endpoints.v1;
public static class CreateProductEndpoint
{
    internal static RouteHandlerBuilder MapProductCreationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/", async (CreateProductCommand request, ISender mediator) =>
            {
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(CreateProductEndpoint))
            .WithSummary("creates a product")
            .WithDescription("creates a product")
            .Produces<CreateProductResponse>()
            .RequirePermission("Permissions.Products.Create")
            .MapToApiVersion(1);
    }
}
