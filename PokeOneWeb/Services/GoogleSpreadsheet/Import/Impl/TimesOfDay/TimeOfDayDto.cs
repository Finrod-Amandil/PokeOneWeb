namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.TimesOfDay
{
    public class TimeOfDayDto : ISpreadsheetEntityDto
    {
        public int SortIndex { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Color { get; set; }
    }
}
