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
    /// <param name="builder">The <see cref="IDataProtectionBuilder" /> to configure.</param>
    /// <param name="connectionString">The SQL Server connection string.</param>
    /// <param name="ensureTableCreated">
    ///     When <see langword="true" /> (the default), attempts to create the
    ///     <c>DataProtectionKeys</c> table if it does not already exist. A warning is logged and
    ///     startup continues if the connection string lacks <c>CREATE TABLE</c> permission.
    ///     Set to <see langword="false" /> to opt out and manage the schema manually.
    /// </param>
    public static IDataProtectionBuilder PersistKeysToSqlServer(this IDataProtectionBuilder builder,
        string connectionString, bool ensureTableCreated = true)
    {
        if (ensureTableCreated)
        {
            using DataProtectionDbContext context = new DataProtectionDbContext(connectionString);
            DataProtectionSchema.EnsureTableExists(context);
        }

        builder.AddKeyManagementOptions(options =>
        {
            options.XmlRepository = new EfXmlRepository(() => new DataProtectionDbContext(connectionString));
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
    /// <param name="builder">The <see cref="IDataProtectionBuilder" /> to configure.</param>
    /// <param name="connectionString">The SQL Server connection string.</param>
    /// <param name="ensureTableCreated">
    ///     When <see langword="true" /> (the default), attempts to create the
    ///     <c>DataProtectionKeys</c> table if it does not already exist. A warning is logged and
    ///     startup continues if the connection string lacks <c>CREATE TABLE</c> permission.
    ///     Set to <see langword="false" /> to opt out and manage the schema manually.
    /// </param>
    public static IDataProtectionBuilder PersistKeysToSqlServer(this IDataProtectionBuilder builder,
        string connectionString, bool ensureTableCreated = true)
    {
        DbContextOptions<DataProtectionDbContext> options = new DbContextOptionsBuilder<DataProtectionDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        if (ensureTableCreated)
        {
            using DataProtectionDbContext context = new DataProtectionDbContext(options);
            DataProtectionSchema.EnsureTableExists(context);
        }

        builder.AddKeyManagementOptions(keyOptions =>
        {
            keyOptions.XmlRepository = new EfXmlRepository(() => new DataProtectionDbContext(options));
        });

        return builder;
    }
}
#endif