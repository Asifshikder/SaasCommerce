using Microsoft.AspNetCore.Identity;

namespace Project.Infrastructure.Identity.RoleClaims;
public class ApplicationRoleClaim : IdentityRoleClaim<string>
{
    public string? CreatedBy { get; init; }
    public DateTimeOffset CreatedOn { get; init; }
}
