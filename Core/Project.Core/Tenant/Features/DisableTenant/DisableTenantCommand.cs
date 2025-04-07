using MediatR;

namespace Project.Core.Tenant.Features.DisableTenant;
public record DisableTenantCommand(string TenantId) : IRequest<DisableTenantResponse>;
