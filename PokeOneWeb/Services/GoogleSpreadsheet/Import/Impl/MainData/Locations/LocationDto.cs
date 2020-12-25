namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.Locations
{
    public class LocationDto : ISpreadsheetEntityDto
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
