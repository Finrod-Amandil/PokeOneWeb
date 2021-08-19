namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Availabilities
{
    public class AvailabilitySheetDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
