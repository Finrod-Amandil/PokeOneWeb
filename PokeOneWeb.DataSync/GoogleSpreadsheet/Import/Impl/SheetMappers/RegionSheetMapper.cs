using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class RegionSheetMapper : SheetMapper<Region>
    {
        public RegionSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<Region, object>> ValueToEntityMappings => new()
        {
            { "Name", (e, v) => e.Name = v.ParseAsNonEmptyString() },
            { "ResourceName", (e, v) => e.ResourceName = v.ParseAsNonEmptyString() },
            { "Color", (e, v) => e.Color = v.ParseAsNonEmptyString() },
            { "Description", (e, v) => e.Description = v.ParseAsString() },
            { "IsReleased", (e, v) => e.IsReleased = v.ParseAsBoolean(defaultValue: true) },
            { "IsMainRegion", (e, v) => e.IsMainRegion = v.ParseAsBoolean() },
            { "IsSideRegion", (e, v) => e.IsSideRegion = v.ParseAsBoolean() },
            { "IsEventRegion", (e, v) => e.IsEventRegion = v.ParseAsBoolean() },
            { "EventName", (e, v) => e.EventName = v.ParseAsOptionalString() },
        };
    }
}