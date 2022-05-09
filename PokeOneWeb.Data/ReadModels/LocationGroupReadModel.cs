using System.Collections.Generic;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    public class LocationGroupListReadModel : IReadModel
    {
        public string Name { get; set; }
        public string ResourceName { get; set; }

        public int SortIndex { get; set; }
        public string RegionResourceName { get; set; }
        public string RegionName { get; set; }
    }

    public class LocationGroupReadModel
    {
        public bool IsEventRegion { get; set; }
        public string EventName { get; set; }
        public string EventStartDate { get; set; }
        public string EventEndDate { get; set; }

        public string PreviousLocationGroupResourceName { get; set; }
        public string PreviousLocationGroupName { get; set; }
        public string NextLocationGroupResourceName { get; set; }
        public string NextLocationGroupName { get; set; }

        public List<LocationReadModel> Locations { get; set; } = new();
    }
}