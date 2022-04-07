using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    public class LocationGroupReadModel : IReadModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required, JsonIgnore]
        public int ApplicationDbId { get; set; }

        public string ResourceName { get; set; }

        public int SortIndex { get; set; }

        public string Name { get; set; }

        public string RegionName { get; set; }

        public string RegionResourceName { get; set; }
    }
}
