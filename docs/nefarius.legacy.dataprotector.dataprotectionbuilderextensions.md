# DataProtectionBuilderExtensions

Namespace: Nefarius.Legacy.DataProtector

[IDataProtectionBuilder](https://learn.microsoft.com/dotnet/api/microsoft.aspnetcore.dataprotection.idataprotectionbuilder) extensions.

```csharp
public static class DataProtectionBuilderExtensions
```

Inheritance [Object](https://learn.microsoft.com/dotnet/api/system.object) → [DataProtectionBuilderExtensions](./nefarius.legacy.dataprotector.dataprotectionbuilderextensions.md)<br>
Attributes [ExtensionAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### <a id="methods-persistkeystosqlserver"/>**PersistKeysToSqlServer(IDataProtectionBuilder, String, Boolean, ILoggerFactory)**

Registers the [EfXmlRepository](./nefarius.legacy.dataprotector.efxmlrepository.md) using EF Core.

```csharp
public static IDataProtectionBuilder PersistKeysToSqlServer(IDataProtectionBuilder builder, string connectionString, bool ensureTableCreated, ILoggerFactory loggerFactory)
```

#### Parameters

`builder` [IDataProtectionBuilder](https://learn.microsoft.com/dotnet/api/microsoft.aspnetcore.dataprotection.idataprotectionbuilder)<br>
The [IDataProtectionBuilder](https://learn.microsoft.com/dotnet/api/microsoft.aspnetcore.dataprotection.idataprotectionbuilder) to configure.

`connectionString` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>
The SQL Server connection string.

`ensureTableCreated` [Boolean](https://learn.microsoft.com/dotnet/api/system.boolean)<br>
When `true` (the default), attempts to create the
 `DataProtectionKeys` table if it does not already exist. A warning is logged at
 `Warning` level via the application's [ILoggerFactory](https://learn.microsoft.com/dotnet/api/microsoft.extensions.logging.iloggerfactory) and startup
 continues if the connection string lacks `CREATE TABLE` permission.
 Set to `false` to opt out and manage the schema manually.

`loggerFactory` [ILoggerFactory](https://learn.microsoft.com/dotnet/api/microsoft.extensions.logging.iloggerfactory)<br>
Optional override [ILoggerFactory](https://learn.microsoft.com/dotnet/api/microsoft.extensions.logging.iloggerfactory). When `null` (the
 default), the factory is resolved from the DI container automatically.

#### Returns

[IDataProtectionBuilder](https://learn.microsoft.com/dotnet/api/microsoft.aspnetcore.dataprotection.idataprotectionbuilder)
