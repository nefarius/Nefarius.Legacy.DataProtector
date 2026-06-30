using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
    /// <param name="loggerFactory">
    ///     Optional <see cref="ILoggerFactory" /> used to surface auto-create failures via the
    ///     application's logger pipeline. When <see langword="null" />, failures fall back to
    ///     <see cref="System.Diagnostics.Trace" />.
    /// </param>
    public static IDataProtectionBuilder PersistKeysToSqlServer(this IDataProtectionBuilder builder,
        string connectionString, bool ensureTableCreated = true, ILoggerFactory? loggerFactory = null)
    {
        if (ensureTableCreated)
        {
            using DataProtectionDbContext context = new DataProtectionDbContext(connectionString);
            DataProtectionSchema.EnsureTableExists(context,
                loggerFactory?.CreateLogger(typeof(DataProtectionSchema).FullName!));
        }

        builder.AddKeyManagementOptions(options =>
        {
            options.XmlRepository =
                new EfXmlRepository(() => new DataProtectionDbContext(connectionString), loggerFactory);
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
    ///     <c>DataProtectionKeys</c> table if it does not already exist. A warning is logged at
    ///     <c>Warning</c> level via the application's <see cref="ILoggerFactory" /> and startup
    ///     continues if the connection string lacks <c>CREATE TABLE</c> permission.
    ///     Set to <see langword="false" /> to opt out and manage the schema manually.
    /// </param>
    /// <param name="loggerFactory">
    ///     Optional override <see cref="ILoggerFactory" />. When <see langword="null" /> (the
    ///     default), the factory is resolved from the DI container automatically.
    /// </param>
    public static IDataProtectionBuilder PersistKeysToSqlServer(this IDataProtectionBuilder builder,
        string connectionString, bool ensureTableCreated = true, ILoggerFactory? loggerFactory = null)
    {
        DbContextOptions<DataProtectionDbContext> options = new DbContextOptionsBuilder<DataProtectionDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        // Defer EnsureTableExists and repository construction to first Options resolution so that
        // the DI-resolved ILoggerFactory is available, giving the consumer a real ILogger channel
        // for any auto-create failure instead of falling back to System.Diagnostics.Trace.
        builder.Services.AddOptions<KeyManagementOptions>()
            .Configure<ILoggerFactory>((keyOptions, diFactory) =>
            {
                ILoggerFactory lf = loggerFactory ?? diFactory;

                if (ensureTableCreated)
                {
                    using DataProtectionDbContext context = new DataProtectionDbContext(options);
                    DataProtectionSchema.EnsureTableExists(context,
                        lf.CreateLogger(typeof(DataProtectionSchema).FullName!));
                }

                keyOptions.XmlRepository = new EfXmlRepository(() => new DataProtectionDbContext(options), lf);
            });

        return builder;
    }
}
#endif