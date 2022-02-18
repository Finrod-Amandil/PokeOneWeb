namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.SeasonTimesOfDay
{
    public class SeasonTimeOfDaySheetDto : ISpreadsheetEntityDto
    {
        public string SeasonName { get; set; }
        public string TimeOfDayName { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
    }
}
