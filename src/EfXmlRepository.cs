﻿// ReSharper disable UnusedType.Global

using System.Xml.Linq;

using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.Logging;
#if NETFRAMEWORK
using System.Data.Entity;
#endif

namespace Nefarius.Legacy.DataProtector;

/// <summary>
///     The XML repository backed by Entity Framework.
/// </summary>
internal class EfXmlRepository : IXmlRepository
{
    private readonly Func<DataProtectionDbContext> _contextFactory;
    private readonly ILogger? _logger;

    internal EfXmlRepository(Func<DataProtectionDbContext> contextFactory, ILoggerFactory? loggerFactory = null)
    {
        _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        _logger = loggerFactory?.CreateLogger<EfXmlRepository>();
    }

    /// <inheritdoc />
    public IReadOnlyCollection<XElement> GetAllElements()
    {
        _logger?.LogDebug("Getting all elements");
        using DataProtectionDbContext? context = _contextFactory();
        return context.DataProtectionKeys
            .Select(k => k.XmlData)
            .ToList()
            .Select(XElement.Parse)
            .ToList();
    }

    /// <inheritdoc />
    public void StoreElement(XElement element, string friendlyName)
    {
        _logger?.LogDebug("Storing element {FriendlyName}.", friendlyName);

        using DataProtectionDbContext? context = _contextFactory();
        DataProtectionKey? keyEntry = context.DataProtectionKeys.Find(friendlyName);

        if (keyEntry == null)
        {
            keyEntry = new DataProtectionKey { Id = friendlyName, XmlData = element.ToString() };
            context.DataProtectionKeys.Add(keyEntry);
        }
        else
        {
            keyEntry.XmlData = element.ToString();
            keyEntry.LastModified = DateTime.UtcNow;

#if NETFRAMEWORK
            context.Entry(keyEntry).State = EntityState.Modified;
#else
            context.Entry(keyEntry).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
#endif
        }

        context.SaveChanges();

        _logger?.LogDebug("Stored element {FriendlyName}.", friendlyName);
    }
}