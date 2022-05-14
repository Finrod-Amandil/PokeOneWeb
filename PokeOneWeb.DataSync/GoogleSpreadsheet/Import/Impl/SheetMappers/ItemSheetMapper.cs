using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class ItemSheetMapper : SheetMapper<Item>
    {
        public ItemSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<Item, object>> ValueToEntityMappings => new()
        {
            { "Name", (e, v) => e.Name = v.ParseAsNonEmptyString() },
            { "IsAvailable", (e, v) => e.IsAvailable = v.ParseAsBoolean() },
            { "DoInclude", (e, v) => e.DoInclude = v.ParseAsBoolean(defaultValue: true) },
            { "ResourceName", (e, v) => e.ResourceName = v.ParseAsNonEmptyString() },
            { "SortIndex", (e, v) => e.SortIndex = v.ParseAsInt() },
            { "BagCategory", (e, v) => e.BagCategoryName = v.ParseAsNonEmptyString() },
            { "PokeoneItemId", (e, v) => e.PokeoneItemId = v.ParseAsOptionalInt() },
            { "Description", (e, v) => e.Description = v.ParseAsString() },
            { "Effect", (e, v) => e.Effect = v.ParseAsString() },
            { "SpriteName", (e, v) => e.SpriteName = v.ParseAsString() },
        };
    }
}