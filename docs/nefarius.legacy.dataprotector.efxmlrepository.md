# EfXmlRepository

Namespace: Nefarius.Legacy.DataProtector

```csharp
public class EfXmlRepository : Microsoft.AspNetCore.DataProtection.Repositories.IXmlRepository
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [EfXmlRepository](./nefarius.legacy.dataprotector.efxmlrepository.md)<br>
Implements IXmlRepository

## Constructors

### <a id="constructors-.ctor"/>**EfXmlRepository(Func&lt;DataProtectionDbContext&gt;)**

```csharp
public EfXmlRepository(Func<DataProtectionDbContext> contextFactory)
```

#### Parameters

`contextFactory` [Func&lt;DataProtectionDbContext&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

## Methods

### <a id="methods-getallelements"/>**GetAllElements()**

```csharp
public IReadOnlyCollection<XElement> GetAllElements()
```

#### Returns

[IReadOnlyCollection&lt;XElement&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)

### <a id="methods-storeelement"/>**StoreElement(XElement, String)**

```csharp
public void StoreElement(XElement element, string friendlyName)
```

#### Parameters

`element` XElement<br>

`friendlyName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
