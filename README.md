# <img src="assets/NSS-128x128.png" align="left" />Nefarius.Legacy.DataProtector

[![.NET](https://github.com/nefarius/Nefarius.Legacy.DataProtector/actions/workflows/build.yml/badge.svg)](https://github.com/nefarius/Nefarius.Legacy.DataProtector/actions/workflows/build.yml)
[![Nuget](https://img.shields.io/nuget/v/Nefarius.Legacy.DataProtector)](https://www.nuget.org/packages/Nefarius.Legacy.DataProtector/)
[![Nuget](https://img.shields.io/nuget/dt/Nefarius.Legacy.DataProtector)](https://www.nuget.org/packages/Nefarius.Legacy.DataProtector/)

[ASP.NET Data Protection](https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-9.0)
key store using EF (Legacy and Core).

## Motivation

Recently I was tasked with the challenge of starting migrating an ASP.NET 4 MVC monolith over to the sacred .NET core
universe.
Since [no direct upgrade paths exist](https://learn.microsoft.com/en-us/aspnet/core/migration/proper-to-2x/?view=aspnetcore-9.0),
a common strategy is to gradually migrate over app logic to a new backend-frontend-combination while keeping the old app
on life support and incorporated with the new software stack.
A major pain-point is modernizing authentication and giving users an acceptable SSO experience.
One such strategy
is [shared auth cookies](https://learn.microsoft.com/en-us/aspnet/core/security/cookie-sharing?view=aspnetcore-9.0)
between the ASP.NET Classic and Core apps.

Session cookies are encrypted by default, so both app environments need to have means to decrypt the session
details.
[ASP.NET Core Data Protection](https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-9.0)
is used to offload encryption and decryption, and the key material can be shared by a common filesystem directory or
other means.

Since I already had to deal with EF (Core) ad an MS SQL Server, I chose to backport an EF-based SQL Server backed key
repository consumable both in ASP.NET Classic and Core from a single class library.

## Usage

The main advantage is getting SSO via shared cookies to work while not relying on a shared directory, but SQL database
instead.
The example below assumes you have an ASP.NET Core backend playing reverse proxy to a legacy ASP.NET 4.x app
and share the user session via cookie that gets decrypted via the DB-backed keys.

### ASP.NET Core (.NET 8)

```csharp
builder.Services.AddDbContext<DataProtectionDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataProtection")));

builder.Services.AddDataProtection()
    .PersistKeysToSqlServer(builder.Configuration.GetConnectionString("DataProtection")!)    
    .SetApplicationName("iis-app-name");

// example to use SSO via shared cookie with ASP.NET 4 
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    // Shared cookie authentication
    .AddCookie(options =>
    {
        options.Cookie.Name = ".AspNet.SharedCookie";
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.Path = "/";
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
    })
```

### ASP.NET 4 (.NET Framework)

```csharp
// assumes OwinStartup is used

string connectionString = ConfigurationManager.ConnectionStrings["DataProtection"].ConnectionString;

app.UseCookieAuthentication(new CookieAuthenticationOptions
{
    AuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
    CookieName = ".AspNet.SharedCookie",
    CookieSameSite = SameSiteMode.Lax,
    SlidingExpiration = true,
    ExpireTimeSpan = TimeSpan.FromMinutes(120),
    TicketDataFormat = new AspNetTicketDataFormat(
        new DataProtectorShim(
            DataProtectionProvider.Create(
                    new DirectoryInfo(
                        /* NOTE: this is just to avoid Argument(Null)Exception, the value not used */
                        Path.GetTempPath()
                    ),
                    builder =>
                    {
                        // keep in sync with core backend
                        builder.SetApplicationName("iis-app-name");
                        // use DB via EF instead of directory
                        builder.Services.Configure<KeyManagementOptions>(options =>
                        {
                            ILoggerFactory loggerFactory = LoggerFactory.Create(lfb =>
                            {
                                lfb.AddDebug(); // Logs to Debug.WriteLine
                            });

                            options.XmlRepository = new EfXmlRepository(
                                () => new DataProtectionDbContext(connectionString),
                                loggerFactory);
                        });
                    })
                .CreateProtector(
                    "Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware",
                    "Cookies",
                    "v2"))),
    CookieManager = new ChunkingCookieManager()
});
```

## Sources & 3rd party credits

- [Key storage providers in ASP.NET Core / Entity Framework Core](https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/implementation/key-storage-providers?view=aspnetcore-8.0&tabs=visual-studio#entity-framework-core)
