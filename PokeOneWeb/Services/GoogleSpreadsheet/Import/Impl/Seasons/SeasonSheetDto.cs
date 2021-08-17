namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Seasons
{
    public class SeasonSheetDto : ISpreadsheetEntityDto
    {
        public int SortIndex { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Color { get; set; }
    }
}
