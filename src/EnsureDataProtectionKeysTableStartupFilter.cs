#if NETCOREAPP

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Nefarius.Legacy.DataProtector;

internal sealed class EnsureDataProtectionKeysTableStartupFilterOptions
{
    public required string ConnectionStringName { get; set; }
}

internal sealed class EnsureDataProtectionKeysTableStartupFilter(
    IConfiguration configuration,
    IOptions<EnsureDataProtectionKeysTableStartupFilterOptions> options) : IStartupFilter
{
    /// <inheritdoc />
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        EnsureTableExists();
        return next;
    }

    private void EnsureTableExists()
    {
        string? connectionString = configuration.GetConnectionString(options.Value.ConnectionStringName);

        ArgumentException.ThrowIfNullOrEmpty(connectionString);
        
        using SqlConnection connection = new(connectionString);
        connection.Open();

        using SqlCommand command = connection.CreateCommand();
        command.CommandText = """
                                  IF NOT EXISTS (
                                      SELECT * FROM INFORMATION_SCHEMA.TABLES 
                                      WHERE TABLE_NAME = 'DataProtectionKeys' AND TABLE_SCHEMA = 'dbo'
                                  )
                                  BEGIN
                                      CREATE TABLE [dbo].[DataProtectionKeys] (
                                          [Id] INT IDENTITY PRIMARY KEY,
                                          [FriendlyName] NVARCHAR(256) NOT NULL,
                                          [Xml] NVARCHAR(MAX) NOT NULL
                                      )
                                  END
                              """;
        command.ExecuteNonQuery();
    }
}
#endif