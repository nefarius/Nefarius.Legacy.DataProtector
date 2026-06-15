# DataProtectionKey

Namespace: Nefarius.Legacy.DataProtector

DB record that represents a stored data protection key.

```csharp
public class DataProtectionKey
```

Inheritance [Object](https://learn.microsoft.com/dotnet/api/system.object) → [DataProtectionKey](./nefarius.legacy.dataprotector.dataprotectionkey.md)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### <a id="properties-id"/>**Id**

Primary key.

```csharp
public string Id { get; set; }
```

#### Property Value

[String](https://learn.microsoft.com/dotnet/api/system.string)<br>

### <a id="properties-lastmodified"/>**LastModified**

Last modified timestamp.

```csharp
public DateTime LastModified { get; set; }
```

#### Property Value

[DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)<br>

### <a id="properties-xmldata"/>**XmlData**

Key XML blob.

```csharp
public string XmlData { get; set; }
```

#### Property Value

[String](https://learn.microsoft.com/dotnet/api/system.string)<br>

## Constructors

### <a id="constructors-.ctor"/>**DataProtectionKey()**

```csharp
public DataProtectionKey()
```
