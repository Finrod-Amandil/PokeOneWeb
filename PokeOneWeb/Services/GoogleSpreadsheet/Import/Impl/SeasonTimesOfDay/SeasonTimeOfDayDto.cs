namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.SeasonTimesOfDay
{
    public class SeasonTimeOfDayDto : ISpreadsheetEntityDto
    {
        public string SeasonName { get; set; }
        public string TimeOfDayName { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
    }
}
