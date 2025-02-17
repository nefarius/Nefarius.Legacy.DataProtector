# DataProtectionDbContext

Namespace: Nefarius.Legacy.DataProtector

Data protection database context.

```csharp
public class DataProtectionDbContext : Microsoft.EntityFrameworkCore.DbContext, Microsoft.EntityFrameworkCore.Infrastructure.IInfrastructure`1[[System.IServiceProvider, System.ComponentModel, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]], Microsoft.EntityFrameworkCore.Internal.IDbContextDependencies, Microsoft.EntityFrameworkCore.Internal.IDbSetCache, Microsoft.EntityFrameworkCore.Internal.IDbContextPoolable, Microsoft.EntityFrameworkCore.Infrastructure.IResettableService, System.IDisposable, System.IAsyncDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → DbContext → [DataProtectionDbContext](./nefarius.legacy.dataprotector.dataprotectiondbcontext.md)<br>
Implements IInfrastructure&lt;IServiceProvider&gt;, IDbContextDependencies, IDbSetCache, IDbContextPoolable, IResettableService, [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable), [IAsyncDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncdisposable)

## Properties

### <a id="properties-changetracker"/>**ChangeTracker**

```csharp
public ChangeTracker ChangeTracker { get; }
```

#### Property Value

ChangeTracker<br>

### <a id="properties-contextid"/>**ContextId**

```csharp
public DbContextId ContextId { get; }
```

#### Property Value

DbContextId<br>

### <a id="properties-database"/>**Database**

```csharp
public DatabaseFacade Database { get; }
```

#### Property Value

DatabaseFacade<br>

### <a id="properties-dataprotectionkeys"/>**DataProtectionKeys**

The key entries in the DB.

```csharp
public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
```

#### Property Value

DbSet&lt;DataProtectionKey&gt;<br>

### <a id="properties-model"/>**Model**

```csharp
public IModel Model { get; }
```

#### Property Value

IModel<br>

## Constructors

### <a id="constructors-.ctor"/>**DataProtectionDbContext(DbContextOptions&lt;DataProtectionDbContext&gt;)**

```csharp
public DataProtectionDbContext(DbContextOptions<DataProtectionDbContext> options)
```

#### Parameters

`options` DbContextOptions&lt;DataProtectionDbContext&gt;<br>

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
