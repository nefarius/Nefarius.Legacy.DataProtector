#if NETCOREAPP

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Nefarius.Legacy.DataProtector;

internal sealed class EnsureDataProtectionKeysTableStartupFilterOptions
{
    public required string ConnectionStringName { get; set; }
}

internal sealed class EnsureDataProtectionKeysTableStartupFilter(
    IServiceProvider serviceProvider) : IStartupFilter
{
    /// <inheritdoc />
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            IConfiguration? configuration = scope.ServiceProvider.GetService<IConfiguration>();
            IOptions<EnsureDataProtectionKeysTableStartupFilterOptions>? options = scope.ServiceProvider
                .GetService<IOptions<EnsureDataProtectionKeysTableStartupFilterOptions>>();

            if (options is null || configuration is null)
            {
                next(app);
                return;
            }

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

            next(app);
        };
    }
}
#endif