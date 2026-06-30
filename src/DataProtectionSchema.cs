// ReSharper disable UnusedType.Global

using System.Diagnostics;
#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
#endif

using Microsoft.Extensions.Logging;

namespace Nefarius.Legacy.DataProtector;

internal static class DataProtectionSchema
{
    private const string CreateTableSql =
        "IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'DataProtectionKeys') " +
        "CREATE TABLE [DataProtectionKeys] (" +
        "[Id] NVARCHAR(200) NOT NULL PRIMARY KEY," +
        "[XmlData] NVARCHAR(MAX) NULL," +
        "[LastModified] DATETIME NOT NULL DEFAULT (GETUTCDATE())" +
        ");";

    private const string FailureMessage =
        "Could not automatically create the DataProtectionKeys table. " +
        "Ensure the connection string has CREATE TABLE permission, or create the table manually using " +
        "the SQL_CreateTable.sql script shipped with the package. " +
        "Pass ensureTableCreated: false to opt out of automatic creation.";

    /// <summary>
    ///     Attempts to create the <c>DataProtectionKeys</c> table if it does not exist.
    ///     Logs a warning via <paramref name="logger" /> when provided, otherwise falls back to
    ///     <see cref="Trace" />, and returns on any failure so startup continues.
    /// </summary>
    internal static void EnsureTableExists(DataProtectionDbContext context, ILogger? logger = null)
    {
        try
        {
#if NETCOREAPP
            context.Database.ExecuteSqlRaw(CreateTableSql);
#elif NETFRAMEWORK
            context.Database.ExecuteSqlCommand(CreateTableSql);
#endif
        }
        catch (Exception ex)
        {
            if (logger is not null)
            {
                logger.LogWarning(ex, "[Nefarius.Legacy.DataProtector] {Message}", FailureMessage);
            }
            else
            {
                Trace.TraceWarning($"[Nefarius.Legacy.DataProtector] {FailureMessage} Exception: {ex.Message}");
            }
        }
    }
}
