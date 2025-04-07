using Project.Core.Tenant.Dtos;
using MediatR;

namespace Project.Core.Tenant.Features.GetTenants;
public sealed class GetTenantsQuery : IRequest<List<TenantDetail>>;
