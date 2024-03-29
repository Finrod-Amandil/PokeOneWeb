﻿using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.Import.Interfaces;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.Import.SheetMappers
{
    public class BagCategorySheetMapper : SheetMapper<BagCategory>
    {
        public BagCategorySheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<BagCategory, object>> ValueToEntityMappings => new()
        {
            { "Name", (e, v) => e.Name = v.ParseAsNonEmptyString() },
            { "SortIndex", (e, v) => e.SortIndex = v.ParseAsInt() },
        };
    }
}