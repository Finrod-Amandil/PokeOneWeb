namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Times
{
    public class TimeDto : ISpreadsheetEntityDto
    {
        public string SeasonName { get; set; }

        public string SeasonAbbreviation { get; set; }

        public string TimeName { get; set; }

        public string TimeAbbreviation { get; set; }

        public int StartHour { get; set; }

        public int EndHour { get; set; }
    }
}
