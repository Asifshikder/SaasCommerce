﻿using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using Project.Core.Exceptions;
using Project.Core.Persistence;
using Project.Core.Tenant.Abstractions;
using Project.Core.Tenant.Dtos;
using Project.Core.Tenant.Features.CreateTenant;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Project.Infrastructure.Tenant.Services;

public sealed class TenantService : ITenantService
{
    private readonly IMultiTenantStore<ApplicationTenantInfo> _tenantStore;
    private readonly IServiceProvider _serviceProvider;

    public TenantService(IMultiTenantStore<ApplicationTenantInfo> tenantStore, IServiceProvider serviceProvider)
    {
        _tenantStore = tenantStore;
        _serviceProvider = serviceProvider;
    }

    public async Task<string> ActivateAsync(string id, CancellationToken cancellationToken)
    {
        var tenant = await GetTenantInfoAsync(id).ConfigureAwait(false);

        if (tenant.IsActive)
        {
            throw new AppException($"tenant {id} is already activated");
        }

        tenant.Activate();

        await _tenantStore.TryUpdateAsync(tenant).ConfigureAwait(false);

        return $"tenant {id} is now activated";
    }

    public async Task<string> CreateAsync(CreateTenantCommand request, CancellationToken cancellationToken)
    {
        var connectionString = PrepareConnectionString(request.Name);
        //if (request.ConnectionString?.Trim() == _config.ConnectionString.Trim())
        //{
        //    connectionString = string.Empty;
        //}
        ApplicationTenantInfo tenant = new(request.Id, request.Name, connectionString, request.AdminEmail, request.Issuer);
        await _tenantStore.TryAddAsync(tenant).ConfigureAwait(false);

        await InitializeDatabase(tenant).ConfigureAwait(false);

        return tenant.Id;
    }

    private string PrepareConnectionString(string? connectionString)
    {
        throw new NotImplementedException();
    }

    private async Task InitializeDatabase(ApplicationTenantInfo tenant)
    {
        // First create a new scope
        using var scope = _serviceProvider.CreateScope();

        // Then set current tenant so the right connection string is used
        scope.ServiceProvider.GetRequiredService<IMultiTenantContextSetter>()
            .MultiTenantContext = new MultiTenantContext<ApplicationTenantInfo>()
            {
                TenantInfo = tenant
            };

        // using the scope, perform migrations / seeding
        var initializers = scope.ServiceProvider.GetServices<IDbInitializer>();
        foreach (var initializer in initializers)
        {
            await initializer.MigrateAsync(CancellationToken.None).ConfigureAwait(false);
            await initializer.SeedAsync(CancellationToken.None).ConfigureAwait(false);
        }
    }

    public async Task<string> DeactivateAsync(string id)
    {
        var tenant = await GetTenantInfoAsync(id).ConfigureAwait(false);
        if (!tenant.IsActive)
        {
            throw new AppException($"tenant {id} is already deactivated");
        }

        tenant.Deactivate();
        await _tenantStore.TryUpdateAsync(tenant).ConfigureAwait(false);
        return $"tenant {id} is now deactivated";
    }

    public async Task<bool> ExistsWithIdAsync(string id) =>
        await _tenantStore.TryGetAsync(id).ConfigureAwait(false) is not null;

    public async Task<bool> ExistsWithNameAsync(string name) =>
        (await _tenantStore.GetAllAsync().ConfigureAwait(false)).Any(t => t.Name == name);

    public async Task<List<TenantDetail>> GetAllAsync()
    {
        var tenants = (await _tenantStore.GetAllAsync().ConfigureAwait(false)).Adapt<List<TenantDetail>>();
        return tenants;
    }

    public async Task<TenantDetail> GetByIdAsync(string id) =>
        (await GetTenantInfoAsync(id).ConfigureAwait(false))
            .Adapt<TenantDetail>();

    public async Task<DateTime> UpgradeSubscription(string id, DateTime extendedExpiryDate)
    {
        var tenant = await GetTenantInfoAsync(id).ConfigureAwait(false);
        tenant.SetValidity(extendedExpiryDate);
        await _tenantStore.TryUpdateAsync(tenant).ConfigureAwait(false);
        return tenant.ValidUpto;
    }

    private async Task<ApplicationTenantInfo> GetTenantInfoAsync(string id) =>
    await _tenantStore.TryGetAsync(id).ConfigureAwait(false)
        ?? throw new NotFoundException($"{typeof(ApplicationTenantInfo).Name} {id} Not Found.");
}
