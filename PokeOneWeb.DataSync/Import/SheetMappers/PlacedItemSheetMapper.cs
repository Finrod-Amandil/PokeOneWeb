using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.Import.Interfaces;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.Import.SheetMappers
{
    public class PlacedItemSheetMapper : SheetMapper<PlacedItem>
    {
        public PlacedItemSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<PlacedItem, object>> ValueToEntityMappings => new()
        {
            { "Location", (e, v) => e.LocationName = v.ParseAsNonEmptyString() },
            { "Quantity", (e, v) => e.Quantity = v.ParseAsInt() },
            { "Item", (e, v) => e.ItemName = v.ParseAsNonEmptyString() },
            { "SortIndex", (e, v) => e.SortIndex = v.ParseAsInt() },
            { "Index", (e, v) => e.Index = v.ParseAsInt() },
            { "PlacementDescription", (e, v) => e.PlacementDescription = v.ParseAsString() },
            { "IsHidden", (e, v) => e.IsHidden = v.ParseAsBoolean(defaultValue: false) },
            { "IsConfirmed", (e, v) => e.IsConfirmed = v.ParseAsBoolean(defaultValue: true) },
            { "Requirements", (e, v) => e.Requirements = v.ParseAsString() },
            { "ScreenshotName", (e, v) => e.ScreenshotName = v.ParseAsString() },
            { "Notes", (e, v) => e.Notes = v.ParseAsString() },
        };
    }
}