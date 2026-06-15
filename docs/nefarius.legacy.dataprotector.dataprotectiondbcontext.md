# DataProtectionDbContext

Namespace: Nefarius.Legacy.DataProtector

Data protection database context.

```csharp
public class DataProtectionDbContext : Microsoft.EntityFrameworkCore.DbContext, Microsoft.EntityFrameworkCore.Infrastructure.IInfrastructure<System.IServiceProvider>, Microsoft.EntityFrameworkCore.Internal.IDbContextDependencies, Microsoft.EntityFrameworkCore.Internal.IDbSetCache, Microsoft.EntityFrameworkCore.Internal.IDbContextPoolable, Microsoft.EntityFrameworkCore.Infrastructure.IResettableService, System.IDisposable, System.IAsyncDisposable
```

Inheritance [Object](https://learn.microsoft.com/dotnet/api/system.object) → [DbContext](https://learn.microsoft.com/dotnet/api/microsoft.entityframeworkcore.dbcontext) → [DataProtectionDbContext](./nefarius.legacy.dataprotector.dataprotectiondbcontext.md)<br>
Implements [IInfrastructure](https://learn.microsoft.com/dotnet/api/microsoft.entityframeworkcore.infrastructure.iinfrastructure-1)<[IServiceProvider](https://learn.microsoft.com/dotnet/api/system.iserviceprovider)>, [IDbContextDependencies](https://learn.microsoft.com/dotnet/api/microsoft.entityframeworkcore.internal.idbcontextdependencies), [IDbSetCache](https://learn.microsoft.com/dotnet/api/microsoft.entityframeworkcore.internal.idbsetcache), [IDbContextPoolable](https://learn.microsoft.com/dotnet/api/microsoft.entityframeworkcore.internal.idbcontextpoolable), [IResettableService](https://learn.microsoft.com/dotnet/api/microsoft.entityframeworkcore.infrastructure.iresettableservice), [IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable), [IAsyncDisposable](https://learn.microsoft.com/dotnet/api/system.iasyncdisposable)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### <a id="properties-changetracker"/>**ChangeTracker**

```csharp
public ChangeTracker ChangeTracker { get; }
```

#### Property Value

[ChangeTracker](https://learn.microsoft.com/dotnet/api/microsoft.entityframeworkcore.changetracking.changetracker)<br>

### <a id="properties-contextid"/>**ContextId**

```csharp
public DbContextId ContextId { get; }
```

#### Property Value

[DbContextId](https://learn.microsoft.com/dotnet/api/microsoft.entityframeworkcore.dbcontextid)<br>

### <a id="properties-database"/>**Database**

```csharp
public DatabaseFacade Database { get; }
```

#### Property Value

[DatabaseFacade](https://learn.microsoft.com/dotnet/api/microsoft.entityframeworkcore.infrastructure.databasefacade)<br>

### <a id="properties-dataprotectionkeys"/>**DataProtectionKeys**

The key entries in the DB.

```csharp
public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
```

#### Property Value

[DbSet](https://learn.microsoft.com/dotnet/api/microsoft.entityframeworkcore.dbset-1)<[DataProtectionKey](./nefarius.legacy.dataprotector.dataprotectionkey.md)><br>

### <a id="properties-model"/>**Model**

```csharp
public IModel Model { get; }
```

#### Property Value

[IModel](https://learn.microsoft.com/dotnet/api/microsoft.entityframeworkcore.metadata.imodel)<br>

## Constructors

### <a id="constructors-.ctor"/>**DataProtectionDbContext(DbContextOptions&lt;DataProtectionDbContext&gt;)**

```csharp
public DataProtectionDbContext(DbContextOptions<DataProtectionDbContext> options)
```

#### Parameters

`options` [DbContextOptions](https://learn.microsoft.com/dotnet/api/microsoft.entityframeworkcore.dbcontextoptions-1)<[DataProtectionDbContext](./nefarius.legacy.dataprotector.dataprotectiondbcontext.md)><br>

## Events

### <a id="events-savechangesfailed"/>**SaveChangesFailed**

```csharp
public event EventHandler<SaveChangesFailedEventArgs> SaveChangesFailed;
```

### <a id="events-savedchanges"/>**SavedChanges**

```csharp
public event EventHandler<SavedChangesEventArgs> SavedChanges;
```

### <a id="events-savingchanges"/>**SavingChanges**

```csharp
public event EventHandler<SavingChangesEventArgs> SavingChanges;
```
