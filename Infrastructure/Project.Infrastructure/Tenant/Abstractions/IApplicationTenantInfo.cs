using Finbuckle.MultiTenant.Abstractions;

namespace Project.Infrastructure.Tenant.Abstractions;
public interface IApplicationTenantInfo : ITenantInfo
{
    string? ConnectionString { get; set; }
}
