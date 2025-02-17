using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;

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
    /// <param name="applicationName">Sets the unique name of this application within the data protection system.</param>
    /// <returns></returns>
    public static IDataProtectionProvider Create(string connectionString, string applicationName)
    {
        return DataProtectionProvider.Create(
            new DirectoryInfo(
                /* NOTE: this is just to avoid Argument(Null)Exception, the value not used */
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
                        () => new DataProtectionDbContext(connectionString));
                });
            });
    }
}
#endif