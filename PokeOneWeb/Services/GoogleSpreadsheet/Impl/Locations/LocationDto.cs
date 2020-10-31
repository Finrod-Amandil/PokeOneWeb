namespace PokeOneWeb.Services.GoogleSpreadsheet.Impl.Locations
{
    public class LocationDto : ISpreadsheetDto
    {
        public string RegionName { get; set; }

        public string LocationGroupName { get; set; }

        public string LocationName { get; set; }

        public int SortIndex { get; set; }

        public bool IsDiscoverable { get; set; }

        public string Notes { get; set; }
    }
}
