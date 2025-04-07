using MediatR;

namespace Project.Core.Tenant.Features.CreateTenant;
public sealed record CreateTenantCommand(string Id,
    string Name,
    string Domain,
    string AdminEmail,
    string? Issuer) : IRequest<CreateTenantResponse>;
