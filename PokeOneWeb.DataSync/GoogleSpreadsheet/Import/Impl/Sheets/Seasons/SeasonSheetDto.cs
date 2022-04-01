namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Seasons
{
    public class SeasonSheetDto : ISpreadsheetEntityDto
    {
        public int SortIndex { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Color { get; set; }
    }
}