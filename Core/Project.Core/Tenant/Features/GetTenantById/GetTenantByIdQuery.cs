using Project.Core.Tenant.Dtos;
using MediatR;

namespace Project.Core.Tenant.Features.GetTenantById;
public record GetTenantByIdQuery(string TenantId) : IRequest<TenantDetail>;
