using PokeOneWeb.Data.ReadModels.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("SimpleLearnableMoveReadModel")]
    public class SimpleLearnableMoveReadModel : IReadModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int ApplicationDbId { get; set; }

        [JsonIgnore]
        public int PokemonVarietyApplicationDbId { get; set; }

        // Variety name
        [NotMapped]
        public string Name { get; set; }

        // Variety resource name
        [NotMapped]
        public string ResourceName { get; set; }

        public string MoveResourceName { get; set; }
    }
}