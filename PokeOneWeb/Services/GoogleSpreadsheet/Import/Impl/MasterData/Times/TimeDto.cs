namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Times
{
    public class TimeDto : ISpreadsheetEntityDto
    {
        public int SeasonSortIndex { get; set; }

        public string SeasonName { get; set; }

        public string SeasonAbbreviation { get; set; }

        public string SeasonColor { get; set; }

        public int TimeSortIndex { get; set; }

        public string TimeName { get; set; }

        public string TimeAbbreviation { get; set; }

        public string TimeColor { get; set; }

        public int StartHour { get; set; }

        public int EndHour { get; set; }
    }
}
