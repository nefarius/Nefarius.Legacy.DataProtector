using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.DataProtection;
#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
#endif

namespace Nefarius.Legacy.DataProtector;

#if NETFRAMEWORK
/// <summary>
///     <see cref="IDataProtectionBuilder" /> extensions.
/// </summary>
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class DataProtectionBuilderExtensions
{
    /// <summary>
    ///     Registers the <see cref="EfXmlRepository" /> using EF 6.
    /// </summary>
    public static IDataProtectionBuilder PersistKeysToSqlServer(this IDataProtectionBuilder builder,
        string connectionString, bool ensureTableExists = true)
    {
        DataProtectionDbContext context = new(connectionString);

        if (ensureTableExists)
        {
            // Explicitly open connection before doing anything else
            DbConnection conn = context.Database.Connection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            using DbCommand command = conn.CreateCommand();
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

        builder.AddKeyManagementOptions(options =>
        {
            options.XmlRepository = new EfXmlRepository(() => context);
        });

        return builder;
    }
}
#endif

#if NETCOREAPP
/// <summary>
///     <see cref="IDataProtectionBuilder" /> extensions.
/// </summary>
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class DataProtectionBuilderExtensions
{
    /// <summary>
    ///     Registers the <see cref="EfXmlRepository" /> using EF Core.
    /// </summary>
    public static IDataProtectionBuilder PersistKeysToSqlServer(this IDataProtectionBuilder builder,
        string connectionString, bool ensureTableExists = true)
    {
        DataProtectionDbContext context = new(
            new DbContextOptionsBuilder<DataProtectionDbContext>()
                .UseSqlServer(connectionString)
                .Options
        );

        if (ensureTableExists)
        {
            // Explicitly open connection before doing anything else
            DbConnection conn = context.Database.GetDbConnection();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            using DbCommand command = conn.CreateCommand();
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

        builder.AddKeyManagementOptions(options =>
        {
            options.XmlRepository = new EfXmlRepository(() => context);
        });

        return builder;
    }
}
#endif