using Project.Core.Paging;
using Project.Infrastructure.Auth.Policy;
using Project.Application.Brands.Get.v1;
using Project.Application.Brands.Search.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Project.Infrastructure.Endpoints.v1;

public static class SearchBrandsEndpoint
{
    internal static RouteHandlerBuilder MapGetBrandListEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/search", async (ISender mediator, [FromBody] SearchBrandsCommand command) =>
            {
                var response = await mediator.Send(command);
                return Results.Ok(response);
            })
            .WithName(nameof(SearchBrandsEndpoint))
            .WithSummary("Gets a list of brands")
            .WithDescription("Gets a list of brands with pagination and filtering support")
            .Produces<PagedList<BrandResponse>>()
            .RequirePermission("Permissions.Brands.View")
            .MapToApiVersion(1);
    }
}
