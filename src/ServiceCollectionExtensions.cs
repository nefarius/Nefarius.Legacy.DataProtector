#if NETCOREAPP
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Nefarius.Legacy.DataProtector;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection EnsureDataProtectionKeysTableExists(this IServiceCollection services,
        string connectionString)
    {
        services.Configure<EnsureDataProtectionKeysTableStartupFilterOptions>(options =>
        {
            options.ConnectionStringName = connectionString;
        });

        return services.AddTransient<IStartupFilter, EnsureDataProtectionKeysTableStartupFilter>();
    }
}
#endif