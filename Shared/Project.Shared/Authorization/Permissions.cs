using System.Collections.ObjectModel;

namespace Project.Shared.Authorization;

public static class Permissions
{
    private static readonly Permission[] AllPermissions =
    [     
        //tenants
        new("View Tenants", Actions.View, Resources.Tenants, IsRoot: true),
        new("Create Tenants", Actions.Create, Resources.Tenants, IsRoot: true),
        new("Update Tenants", Actions.Update, Resources.Tenants, IsRoot: true),
        new("Upgrade Tenant Subscription", Actions.UpgradeSubscription, Resources.Tenants, IsRoot: true),

        //identity
        new("View Users", Actions.View, Resources.Users),
        new("Search Users", Actions.Search, Resources.Users),
        new("Create Users", Actions.Create, Resources.Users),
        new("Update Users", Actions.Update, Resources.Users),
        new("Delete Users", Actions.Delete, Resources.Users),
        new("Export Users", Actions.Export, Resources.Users),
        new("View UserRoles", Actions.View, Resources.UserRoles),
        new("Update UserRoles", Actions.Update, Resources.UserRoles),
        new("View Roles", Actions.View, Resources.Roles),
        new("Create Roles", Actions.Create, Resources.Roles),
        new("Update Roles", Actions.Update, Resources.Roles),
        new("Delete Roles", Actions.Delete, Resources.Roles),
        new("View RoleClaims", Actions.View, Resources.RoleClaims),
        new("Update RoleClaims", Actions.Update, Resources.RoleClaims),
        
        //products
        new("View Products", Actions.View, Resources.Products, IsBasic: true),
        new("Search Products", Actions.Search, Resources.Products, IsBasic: true),
        new("Create Products", Actions.Create, Resources.Products),
        new("Update Products", Actions.Update, Resources.Products),
        new("Delete Products", Actions.Delete, Resources.Products),
        new("Export Products", Actions.Export, Resources.Products),

        //brands
        new("View Brands", Actions.View, Resources.Brands, IsBasic: true),
        new("Search Brands", Actions.Search, Resources.Brands, IsBasic: true),
        new("Create Brands", Actions.Create, Resources.Brands),
        new("Update Brands", Actions.Update, Resources.Brands),
        new("Delete Brands", Actions.Delete, Resources.Brands),
        new("Export Brands", Actions.Export, Resources.Brands),

        //todos
        new("View Todos", Actions.View, Resources.Todos, IsBasic: true),
        new("Search Todos", Actions.Search, Resources.Todos, IsBasic: true),
        new("Create Todos", Actions.Create, Resources.Todos),
        new("Update Todos", Actions.Update, Resources.Todos),
        new("Delete Todos", Actions.Delete, Resources.Todos),
        new("Export Todos", Actions.Export, Resources.Todos),

         new("View Hangfire", Actions.View, Resources.Hangfire),
         new("View Dashboard", Actions.View, Resources.Dashboard),

        //audit
        new("View Audit Trails", Actions.View, Resources.AuditTrails),
    ];

    public static IReadOnlyList<Permission> All { get; } = new ReadOnlyCollection<Permission>(AllPermissions);
    public static IReadOnlyList<Permission> Root { get; } = new ReadOnlyCollection<Permission>(AllPermissions.Where(p => p.IsRoot).ToArray());
    public static IReadOnlyList<Permission> Admin { get; } = new ReadOnlyCollection<Permission>(AllPermissions.Where(p => !p.IsRoot).ToArray());
    public static IReadOnlyList<Permission> Basic { get; } = new ReadOnlyCollection<Permission>(AllPermissions.Where(p => p.IsBasic).ToArray());
}

public record Permission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource)
    {
        return $"Permissions.{resource}.{action}";
    }
}


