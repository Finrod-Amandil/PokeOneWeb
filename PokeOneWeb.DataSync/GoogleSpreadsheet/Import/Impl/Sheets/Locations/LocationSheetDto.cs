namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Locations
{
    public class LocationSheetDto : XISpreadsheetEntityDto
    {
        public string RegionName { get; set; }

        public string LocationGroupName { get; set; }

        public string ResourceName { get; set; }

        public string LocationName { get; set; }

        public int SortIndex { get; set; }

        public bool IsDiscoverable { get; set; }

        public string Notes { get; set; }
    }
}