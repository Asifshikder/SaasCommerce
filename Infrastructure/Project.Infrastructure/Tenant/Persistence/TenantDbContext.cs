using Finbuckle.MultiTenant.EntityFrameworkCore.Stores.EFCoreStore;
using Microsoft.EntityFrameworkCore;

namespace Project.Infrastructure.Tenant.Persistence;
public class TenantDbContext : EFCoreStoreDbContext< ApplicationTenantInfo>
{
    public const string Schema = "tenant";
    public TenantDbContext(DbContextOptions<TenantDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity< ApplicationTenantInfo>().ToTable("Tenants", Schema);
    }
}
