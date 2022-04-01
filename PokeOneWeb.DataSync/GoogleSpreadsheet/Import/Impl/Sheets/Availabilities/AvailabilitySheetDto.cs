namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Availabilities
{
    public class AvailabilitySheetDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}