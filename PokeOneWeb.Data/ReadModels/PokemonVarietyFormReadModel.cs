using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("PokemonVarietyFormReadModel")]
    public class PokemonVarietyFormReadModel : IReadModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; }

        public int SortIndex { get; set; }

        public string SpriteName { get; set; }

        public string Availability { get; set; }
    }
}