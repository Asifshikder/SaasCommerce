using Project.Core.Exceptions;
using System.Security.Claims;
using Project.Core.Identity.Users.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Project.Shared.Authorization;

namespace Project.Infrastructure.Identity.Users.Endpoints;
public static class GetUserProfileEndpoint
{
    internal static RouteHandlerBuilder MapGetMeEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/profile", async (ClaimsPrincipal user, IUserService service, CancellationToken cancellationToken) =>
        {
            if (user.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedException();
            }

            return await service.GetAsync(userId, cancellationToken);
        })
        .WithName("GetMeEndpoint")
        .WithSummary("Get current user information based on token")
        .WithDescription("Get current user information based on token");
    }
}
