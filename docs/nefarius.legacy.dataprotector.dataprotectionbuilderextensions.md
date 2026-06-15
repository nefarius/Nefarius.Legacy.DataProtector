# DataProtectionBuilderExtensions

Namespace: Nefarius.Legacy.DataProtector

[IDataProtectionBuilder](https://learn.microsoft.com/dotnet/api/microsoft.aspnetcore.dataprotection.idataprotectionbuilder) extensions.

```csharp
public static class DataProtectionBuilderExtensions
```

Inheritance [Object](https://learn.microsoft.com/dotnet/api/system.object) → [DataProtectionBuilderExtensions](./nefarius.legacy.dataprotector.dataprotectionbuilderextensions.md)<br>
Attributes [ExtensionAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### <a id="methods-persistkeystosqlserver"/>**PersistKeysToSqlServer(IDataProtectionBuilder, String)**

Registers the [EfXmlRepository](./nefarius.legacy.dataprotector.efxmlrepository.md) using EF Core.

```csharp
public static IDataProtectionBuilder PersistKeysToSqlServer(IDataProtectionBuilder builder, string connectionString)
```

#### Parameters

`builder` [IDataProtectionBuilder](https://learn.microsoft.com/dotnet/api/microsoft.aspnetcore.dataprotection.idataprotectionbuilder)<br>

`connectionString` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

#### Returns

[IDataProtectionBuilder](https://learn.microsoft.com/dotnet/api/microsoft.aspnetcore.dataprotection.idataprotectionbuilder)
