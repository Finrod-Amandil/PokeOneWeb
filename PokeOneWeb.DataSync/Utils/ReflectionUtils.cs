using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PokeOneWeb.Data.Attributes;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;

namespace PokeOneWeb.DataSync.Utils
{
    public static class ReflectionUtils
    {
        public static Dictionary<string, ISheetImporter> LoadSheetImporters(IServiceProvider serviceProvider)
        {
            var importersForSheetNames = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsDefined(typeof(SheetAttribute)))
                .Select(t => new { Type = t, Attribute = t.GetCustomAttribute<SheetAttribute>() })
                .Where(t => t.Attribute != null)
                .ToDictionary(
                    t => t.Attribute.SheetName,
                    t => serviceProvider.GetRequiredService(
                        typeof(SheetImporter<>).MakeGenericType(t.Type)) as ISheetImporter);

            return importersForSheetNames;
        }
    }
}