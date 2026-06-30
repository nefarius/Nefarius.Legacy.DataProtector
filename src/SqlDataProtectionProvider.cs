using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#if NETFRAMEWORK
namespace Nefarius.Legacy.DataProtector;

/// <summary>
///     Alternate <see cref="DataProtectionProvider" /> that uses <see cref="EfXmlRepository" /> as a backing store.
/// </summary>
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class SqlDataProtectionProvider
{
    /// <summary>
    ///     Creates an <see cref="T:Microsoft.AspNetCore.DataProtection.DataProtectionProvider" /> given an SQL server database
    ///     at which to store keys.
    /// </summary>
    /// <param name="connectionString">The SQL Server connection string.</param>
    /// <param name="applicationName">The unique name of this application within the data protection system.</param>
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
    public static IDataProtectionProvider Create(string connectionString, string applicationName,
        bool ensureTableCreated = true, ILoggerFactory? loggerFactory = null)
    {
        if (ensureTableCreated)
        {
            using DataProtectionDbContext context = new DataProtectionDbContext(connectionString);
            DataProtectionSchema.EnsureTableExists(context,
                loggerFactory?.CreateLogger(typeof(DataProtectionSchema).FullName!));
        }

        return DataProtectionProvider.Create(
            new DirectoryInfo(
                /* NOTE: this is just to avoid Argument(Null)Exception, the value is not used */
                Path.GetTempPath()
            ),
            builder =>
            {
                // keep in sync with core backend
                builder.SetApplicationName(applicationName);
                // use DB via EF instead of directory
                builder.Services.Configure<KeyManagementOptions>(options =>
                {
                    options.XmlRepository = new EfXmlRepository(
                        () => new DataProtectionDbContext(connectionString), loggerFactory);
                });
            });
    }
}
#endif