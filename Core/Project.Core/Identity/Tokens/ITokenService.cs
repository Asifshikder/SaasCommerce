using Project.Core.Identity.Tokens.Features.Generate;
using Project.Core.Identity.Tokens.Features.Refresh;
using Project.Core.Identity.Tokens.Models;

namespace Project.Core.Identity.Tokens;
public interface ITokenService
{
    Task<TokenResponse> GenerateTokenAsync(TokenGenerationCommand request, string ipAddress, CancellationToken cancellationToken);
    Task<TokenResponse> RefreshTokenAsync(RefreshTokenCommand request, string ipAddress, CancellationToken cancellationToken);

}
