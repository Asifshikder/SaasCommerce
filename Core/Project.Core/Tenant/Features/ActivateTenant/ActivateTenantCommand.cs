using MediatR;

namespace Project.Core.Tenant.Features.ActivateTenant;
public record ActivateTenantCommand(string TenantId) : IRequest<ActivateTenantResponse>;
