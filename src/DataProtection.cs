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

/// <summary>
///     DB record that represents a stored data protection key.
/// </summary>
public class DataProtectionKey
{
    /// <summary>
    ///     Primary key.
    /// </summary>
    [Key]
    [MaxLength(200)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    ///     Key XML blob.
    /// </summary>
    public string XmlData { get; set; } = string.Empty;

    /// <summary>
    ///     Last modified timestamp.
    /// </summary>
    public DateTime LastModified { get; set; } = DateTime.UtcNow;
}

#if NETFRAMEWORK
/// <summary>
///     Data protection database context.
/// </summary>
public class DataProtectionDbContext : DbContext
{
    /// <inheritdoc />
    public DataProtectionDbContext(string connectionString) : base(connectionString) { }

    /// <summary>
    ///     The key entries in the DB.
    /// </summary>
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = null!;
}
#endif

#if NETCOREAPP
/// <summary>
///     Data protection database context.
/// </summary>
public class DataProtectionDbContext : DbContext
{
    /// <inheritdoc />
    public DataProtectionDbContext(DbContextOptions<DataProtectionDbContext> options)
        : base(options) { }

    /// <summary>
    ///     The key entries in the DB.
    /// </summary>
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
}
#endif