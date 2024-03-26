using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.Import.Interfaces;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.Import.SheetMappers
{
    public class LocationSheetMapper : SheetMapper<Location>
    {
        public LocationSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<Location, object>> ValueToEntityMappings => new()
        {
            { "Region", (e, v) => e.LocationGroup.RegionName = v.ParseAsNonEmptyString() },
            { "LocationGroup", (e, v) => e.LocationGroup.Name = v.ParseAsNonEmptyString() },
            { "ResourceName", (e, v) => e.LocationGroup.ResourceName = v.ParseAsNonEmptyString() },
            { "Name", (e, v) => e.Name = v.ParseAsNonEmptyString() },
            { "SortIndex", (e, v) => e.SortIndex = v.ParseAsInt() },
            { "IsDiscoverable", (e, v) => e.IsDiscoverable = v.ParseAsBoolean(defaultValue: false) },
            { "Notes", (e, v) => e.Notes = v.ParseAsString() },
        };
    }
}