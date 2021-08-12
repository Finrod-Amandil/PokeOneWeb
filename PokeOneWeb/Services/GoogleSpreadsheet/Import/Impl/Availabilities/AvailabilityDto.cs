namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Availabilities
{
    public class AvailabilityDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
