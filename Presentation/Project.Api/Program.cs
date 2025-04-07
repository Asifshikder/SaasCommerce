using Serilog;
using Webapi;
using Project.Infrastructure.Logging.Serilog;
using Project.Infrastructure;
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.ConfigureInfrastructure();
    builder.RegisterModules();

    var app = builder.Build();

    app.UseInfrastructure();
    app.UseModules();
    await app.RunAsync();
}
catch (Exception ex) when (!ex.GetType().Name.Equals("HostAbortedException", StringComparison.Ordinal))
{
    StaticLogger.EnsureInitialized();
    Log.Fatal(ex.Message, "unhandled exception");

}
//finally
//{
//    StaticLogger.EnsureInitialized();
//    Log.Information("server shutting down..");
//    await Log.CloseAndFlushAsync();
//}
