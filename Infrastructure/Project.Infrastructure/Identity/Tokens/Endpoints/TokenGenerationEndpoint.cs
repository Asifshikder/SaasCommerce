using Project.Core.Identity.Tokens;
using Project.Core.Identity.Tokens.Features.Generate;
using Project.Shared.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Project.Infrastructure.Identity.Tokens.Endpoints;
public static class TokenGenerationEndpoint
{
    internal static RouteHandlerBuilder MapTokenGenerationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/", (TokenGenerationCommand request,
            [FromHeader(Name = TenantConstants.Identifier)] string tenant,
            ITokenService service,
            HttpContext context,
            CancellationToken cancellationToken) =>
        {
            string ip = context.GetIpAddress();
            return service.GenerateTokenAsync(request, ip!, cancellationToken);
        })
        .WithName(nameof(TokenGenerationEndpoint))
        .WithSummary("generate JWTs")
        .WithDescription("generate JWTs")
        .AllowAnonymous();
    }
}
