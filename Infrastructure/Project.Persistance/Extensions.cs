using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Project.Core.Persistence;
using Project.Domain;
using Project.Infrastructure.Persistence;
using Project.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistance
{
    public static class Extensions
    {
        public static WebApplicationBuilder RegisterPersistenceServices(this WebApplicationBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);
            builder.Services.BindDbContext<ApplicationDbContext>();
            builder.Services.AddScoped<IDbInitializer, ApplicationDbContextInitializer>();
            return builder;
        }
        public static WebApplication UsePersistenceModule(this WebApplication app)
        {
            return app;
        }
    }
}
