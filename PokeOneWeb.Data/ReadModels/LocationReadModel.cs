using System.Collections.Generic;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    public class LocationReadModel : IReadModel
    {
        public string Name { get; set; }
        public int SortIndex { get; set; }
        public bool IsDiscoverable { get; set; }

        public string Notes { get; set; }

        public List<SpawnReadModel> Spawns { get; set; } = new();
        public List<PlacedItemReadModel> PlacedItems { get; set; } = new();
    }
}