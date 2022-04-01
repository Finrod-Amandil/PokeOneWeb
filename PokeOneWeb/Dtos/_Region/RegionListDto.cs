using System;

namespace PokeOneWeb.WebApi.Dtos
{
    public class RegionListDto
    {
        public string Name { get; set; }

        public string ResourceName { get; set; }

        public bool IsEventRegion { get; set; }

        public string EventName { get; set; }

        public DateTime? EventStartDate { get; set; }

        public DateTime? EventEndDate { get; set; }

        public string Color { get; set; }

        public string Description { get; set; }

        public bool IsReleased { get; set; }

        public bool IsMainRegion { get; set; }

        public bool IsSideRegion { get; set; }
    }
}