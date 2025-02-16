# <img src="assets/NSS-128x128.png" align="left" />Nefarius.Legacy.DataProtector

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