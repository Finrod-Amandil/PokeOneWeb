namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Regions
{
    public class RegionSheetDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsEventRegion { get; set; }
        public string EventName { get; set; }
    }
}
