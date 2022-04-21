namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Regions
{
    public class RegionSheetDto : XISpreadsheetEntityDto
    {
        public string Name { get; set; }
        public string ResourceName { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public bool IsReleased { get; set; }
        public bool IsMainRegion { get; set; }
        public bool IsSideRegion { get; set; }
        public bool IsEventRegion { get; set; }
        public string EventName { get; set; }
    }
}