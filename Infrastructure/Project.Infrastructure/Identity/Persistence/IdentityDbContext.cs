using Finbuckle.MultiTenant.Abstractions;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Project.Core.Audit;
using Project.Core.Persistence;
using Project.Infrastructure.Identity.RoleClaims;
using Project.Infrastructure.Identity.Roles;
using Project.Infrastructure.Identity.Users;
using Project.Infrastructure.Persistence;
using Project.Infrastructure.Tenant;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Project.Infrastructure.Identity.Persistence;
public class IdentityDbContext : MultiTenantIdentityDbContext<ApplicationUser,
    ApplicationRole,
    string,
    IdentityUserClaim<string>,
    IdentityUserRole<string>,
    IdentityUserLogin<string>,
    ApplicationRoleClaim,
    IdentityUserToken<string>>
{

    private new ApplicationTenantInfo TenantInfo { get; set; }
    public IdentityDbContext(IMultiTenantContextAccessor<ApplicationTenantInfo> multiTenantContextAccessor, DbContextOptions<IdentityDbContext> options, IOptions<DatabaseOptions> settings) : base(multiTenantContextAccessor, options)
    {
        TenantInfo = multiTenantContextAccessor.MultiTenantContext.TenantInfo!;
    }

    public DbSet<AuditTrail> AuditTrails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!string.IsNullOrWhiteSpace(TenantInfo?.ConnectionString))
        {
            optionsBuilder.ConfigureDatabase(TenantInfo.ConnectionString);
        }
    }
}
