# DataProtectionBuilderExtensions

Namespace: Nefarius.Legacy.DataProtector

extensions.

```csharp
public static class DataProtectionBuilderExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DataProtectionBuilderExtensions](./nefarius.legacy.dataprotector.dataprotectionbuilderextensions.md)

## Methods

### <a id="methods-persistkeystosqlserver"/>**PersistKeysToSqlServer(IDataProtectionBuilder, String)**

Registers the [EfXmlRepository](./nefarius.legacy.dataprotector.efxmlrepository.md) using EF Core.

```csharp
public static IDataProtectionBuilder PersistKeysToSqlServer(IDataProtectionBuilder builder, string connectionString)
```

#### Parameters

`builder` IDataProtectionBuilder<br>

`connectionString` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

IDataProtectionBuilder
