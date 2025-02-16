// ReSharper disable UnusedType.Global
#if NETFRAMEWORK
using System.Data.Entity;
#endif
using System.Xml.Linq;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.Logging;

namespace Nefarius.Legacy.DataProtector;

public class EfXmlRepository : IXmlRepository
{
    private readonly Func<DataProtectionDbContext> _contextFactory;
    private readonly ILogger? _logger;

    public EfXmlRepository(Func<DataProtectionDbContext> contextFactory, ILoggerFactory? loggerFactory = null)
    {
        _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        _logger = loggerFactory?.CreateLogger<EfXmlRepository>();
    }

    /// <inheritdoc />
    public IReadOnlyCollection<XElement> GetAllElements()
    {
        _logger?.LogDebug("Getting all elements");
        using var context = _contextFactory();
        return context.DataProtectionKeys
            .Select(k => XElement.Parse(k.XmlData))
            .ToList();
    }

    /// <inheritdoc />
    public void StoreElement(XElement element, string friendlyName)
    {
        _logger?.LogDebug($"Storing element {friendlyName}.");
        
        using var context = _contextFactory();
        var keyEntry = context.DataProtectionKeys.Find(friendlyName);

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
        
        _logger?.LogDebug($"Stored element {friendlyName}.");
    }
}