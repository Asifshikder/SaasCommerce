using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Project.Core.Persistence;
using Project.Domain;
using Project.Infrastructure.Endpoints.v1;
using Project.Infrastructure.Persistence;
using Project.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application
{
    public static class CatalogModule
    {
        public class Endpoints : CarterModule
        {
            public Endpoints() : base("catalog") { }
            public override void AddRoutes(IEndpointRouteBuilder app)
            {
                var productGroup = app.MapGroup("products").WithTags("products");
                productGroup.MapProductCreationEndpoint();
                productGroup.MapGetProductEndpoint();
                productGroup.MapGetProductListEndpoint();
                productGroup.MapProductUpdateEndpoint();
                productGroup.MapProductDeleteEndpoint();

                var brandGroup = app.MapGroup("brands").WithTags("brands");
                brandGroup.MapBrandCreationEndpoint();
                brandGroup.MapGetBrandEndpoint();
                brandGroup.MapGetBrandListEndpoint();
                brandGroup.MapBrandUpdateEndpoint();
                brandGroup.MapBrandDeleteEndpoint();
            }
        }
        public static WebApplicationBuilder RegisterCatalogServices(this WebApplicationBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);
            builder.Services.AddKeyedScoped<IRepository<Product>, EntityRepository<Product>>("catalog:products");
            builder.Services.AddKeyedScoped<IReadRepository<Product>, EntityRepository<Product>>("catalog:products");
            builder.Services.AddKeyedScoped<IRepository<Brand>, EntityRepository<Brand>>("catalog:brands");
            builder.Services.AddKeyedScoped<IReadRepository<Brand>, EntityRepository<Brand>>("catalog:brands");
            return builder;
        }
        public static WebApplication UseCatalogModule(this WebApplication app)
        {
            return app;
        }
    }

}
