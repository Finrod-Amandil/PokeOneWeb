using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("EvolutionAbilityReadModel")]
    public class EvolutionAbilityReadModel : IReadModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        public int RelativeStageIndex { get; set; }
        public string PokemonResourceName { get; set; }
        public int PokemonSortIndex { get; set; }
        public string PokemonName { get; set; }
        public string SpriteName { get; set; }
        public string AbilityName { get; set; }

        [JsonIgnore]
        public int? PokemonVarietyAsPrimaryAbilityId { get; set; }

        [JsonIgnore]
        public int? PokemonVarietyAsSecondaryAbilityId { get; set; }

        [JsonIgnore]
        public int? PokemonVarietyAsHiddenAbilityId { get; set; }
    }
}