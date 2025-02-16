# <img src="assets/NSS-128x128.png" align="left" />Nefarius.Legacy.DataProtector

[![.NET](https://github.com/nefarius/Nefarius.Legacy.DataProtector/actions/workflows/build.yml/badge.svg)](https://github.com/nefarius/Nefarius.Legacy.DataProtector/actions/workflows/build.yml)
[![Nuget](https://img.shields.io/nuget/v/Nefarius.Legacy.DataProtector)](https://www.nuget.org/packages/Nefarius.Legacy.DataProtector/)
[![Nuget](https://img.shields.io/nuget/dt/Nefarius.Legacy.DataProtector)](https://www.nuget.org/packages/Nefarius.Legacy.DataProtector/)

[ASP.NET Data Protection](https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-9.0)
key store using EF (Legacy and Core).

## Usage

### ASP.NET Core (.NET 8)

```csharp
services.AddDbContext<DataProtectionDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

services.AddDataProtection()
    .PersistKeysToRepository(new EfXmlRepository(() => new DataProtectionDbContext(
        new DbContextOptionsBuilder<DataProtectionDbContext>()
            .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            .Options
    )));
```

### ASP.NET 4 (.NET Framework)

```csharp
var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

var dataProtectionRepository = new EfXmlRepository(() => new DataProtectionDbContext(connectionString));

// Example usage:
var dataProtectionProvider = DataProtectionProvider.Create("MyApp");
var protector = dataProtectionProvider.CreateProtector("MyPurpose");

string protectedData = protector.Protect("SensitiveData");
string unprotectedData = protector.Unprotect(protectedData);
```