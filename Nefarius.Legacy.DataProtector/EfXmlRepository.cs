// ReSharper disable UnusedType.Global
#if NETFRAMEWORK
using System.Data.Entity;
#endif
using System.Xml.Linq;
using Microsoft.AspNetCore.DataProtection.Repositories;

namespace Nefarius.Legacy.DataProtector;

public class EfXmlRepository : IXmlRepository
{
    private readonly Func<DataProtectionDbContext> _contextFactory;

    public EfXmlRepository(Func<DataProtectionDbContext> contextFactory)
    {
        _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
    }

    public IReadOnlyCollection<XElement> GetAllElements()
    {
        using var context = _contextFactory();
        return context.DataProtectionKeys
            .Select(k => XElement.Parse(k.XmlData))
            .ToList();
    }

    public void StoreElement(XElement element, string friendlyName)
    {
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
    }
}