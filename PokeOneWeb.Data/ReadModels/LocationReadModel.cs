using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("LocationReadModel")]
    public class LocationReadModel

    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        [JsonIgnore]
        public int ApplicationDbId { get; set; }

        public string Name { get; set; }

        public int SortIndex { get; set; }

        public bool IsDiscoverable { get; set; }

        public string Notes { get; set; }

        public List<SpawnReadModel> Spawns { get; set; } = new();

        public List<PlacedItemReadModel> PlacedItems { get; set; } = new();
    }
}