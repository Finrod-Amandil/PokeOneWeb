﻿namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.PvpTiers
{
    public class PvpTierSheetDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }

        public int SortIndex { get; set; }
    }
}