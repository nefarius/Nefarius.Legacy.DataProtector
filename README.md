# <img src="assets/NSS-128x128.png" align="left" />Nefarius.Legacy.DataProtector

[![.NET](https://github.com/nefarius/Nefarius.Legacy.DataProtector/actions/workflows/build.yml/badge.svg)](https://github.com/nefarius/Nefarius.Legacy.DataProtector/actions/workflows/build.yml)
[![Nuget](https://img.shields.io/nuget/v/Nefarius.Legacy.DataProtector)](https://www.nuget.org/packages/Nefarius.Legacy.DataProtector/)
[![Nuget](https://img.shields.io/nuget/dt/Nefarius.Legacy.DataProtector)](https://www.nuget.org/packages/Nefarius.Legacy.DataProtector/)

[ASP.NET Data Protection](https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-9.0)
key store using EF (Legacy and Core).

## Usage

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
