using System;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.Regions
{
    public class RegionDto : ISpreadsheetEntityDto
    {
        public string RegionName { get; set; }
        public bool IsEventRegion { get; set; }
        public string EventName { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
    }
}
