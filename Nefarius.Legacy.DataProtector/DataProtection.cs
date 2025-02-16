// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedType.Global
using System;
using System.ComponentModel.DataAnnotations;
#if NETFRAMEWORK
using System.Data.Entity;
#endif
#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
#endif

namespace Nefarius.Legacy.DataProtector;

public class DataProtectionKey
{
    [Key]
    [MaxLength(200)]
    public string Id { get; set; } = string.Empty;

    public string XmlData { get; set; } = string.Empty;

    public DateTime LastModified { get; set; } = DateTime.UtcNow;
}

#if NETFRAMEWORK
public class DataProtectionDbContext : DbContext
{
    public DataProtectionDbContext(string connectionString) : base(connectionString) { }

    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
}
#endif

#if NETCOREAPP
public class DataProtectionDbContext : DbContext
{
    public DataProtectionDbContext(DbContextOptions<DataProtectionDbContext> options)
        : base(options) { }

    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
}
#endif