# DataProtectionKey

Namespace: Nefarius.Legacy.DataProtector

DB record that represents a stored data protection key.

```csharp
public class DataProtectionKey
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DataProtectionKey](./nefarius.legacy.dataprotector.dataprotectionkey.md)

## Properties

### <a id="properties-id"/>**Id**

Primary key.

```csharp
public string Id { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-lastmodified"/>**LastModified**

Last modified timestamp.

```csharp
public DateTime LastModified { get; set; }
```

#### Property Value

[DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

### <a id="properties-xmldata"/>**XmlData**

Key XML blob.

```csharp
public string XmlData { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### <a id="constructors-.ctor"/>**DataProtectionKey()**

```csharp
public DataProtectionKey()
```
