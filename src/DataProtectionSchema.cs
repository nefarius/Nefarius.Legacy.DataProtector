// ReSharper disable UnusedType.Global

using System.Diagnostics;
#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
#endif

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

    /// <summary>
    ///     Attempts to create the <c>DataProtectionKeys</c> table if it does not exist.
    ///     Logs a warning via <see cref="Trace" /> and returns on any failure.
    /// </summary>
    internal static void EnsureTableExists(DataProtectionDbContext context)
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
            Trace.TraceWarning(
                "[Nefarius.Legacy.DataProtector] Could not automatically create the DataProtectionKeys table. " +
                "Ensure the connection string has CREATE TABLE permission, or create the table manually using " +
                "the SQL_CreateTable.sql script shipped with the package. " +
                $"Exception: {ex.Message}");
        }
    }
}
