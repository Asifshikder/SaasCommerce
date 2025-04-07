using Project.Core.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Project.Infrastructure.Persistence.Services;
internal sealed class ConnectionStringValidator(ILogger<ConnectionStringValidator> logger) : IConnectionStringValidator
{
    private readonly ILogger<ConnectionStringValidator> _logger = logger;

    public bool TryValidate(string connectionString)
    {
        try
        {
            new SqlConnectionStringBuilder(connectionString);

            return true;
        }
        catch (Exception ex)
        {
#pragma warning disable S6667 // Logging in a catch clause should pass the caught exception as a parameter.
            _logger.LogError("Connection String Validation Exception : {Error}", ex.Message);
#pragma warning restore S6667 // Logging in a catch clause should pass the caught exception as a parameter.
            return false;
        }
    }
}
