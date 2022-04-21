namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.TimesOfDay
{
    public class TimeOfDaySheetDto : XISpreadsheetEntityDto
    {
        public int SortIndex { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Color { get; set; }
    }
}