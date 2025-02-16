using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.DataProtection;
#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
#endif

namespace Nefarius.Legacy.DataProtector;

#if NETCOREAPP
/// <summary>
///     <see cref="IDataProtectionBuilder"/> extensions.
/// </summary>
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class DataProtectionBuilderExtensions
{
    /// <summary>
    ///     Registers the <see cref="EfXmlRepository"/> using EF Core.
    /// </summary>
    public static IDataProtectionBuilder PersistKeysToFileSystem(this IDataProtectionBuilder builder,
        string connectionString)
    {
        builder.AddKeyManagementOptions(options =>
        {
            options.XmlRepository = new EfXmlRepository(() => new DataProtectionDbContext(
                new DbContextOptionsBuilder<DataProtectionDbContext>()
                    .UseSqlServer(connectionString)
                    .Options
            ));
        });

        return builder;
    }
}
#endif