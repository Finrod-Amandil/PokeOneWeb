using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("AbilityTurnsIntoReadModel")]
    public class AbilityTurnsIntoReadModel
    {
        public int Id { get; set; }

        public string PokemonResourceName { get; set; }
        public int PokemonSortIndex { get; set; }
        public string PokemonName { get; set; }
        public string AbilityName { get; set; }

        public int? PokemonVarietyAsPrimaryAbilityId { get; set; }
        public int? PokemonVarietyAsSecondaryAbilityId { get; set; }
        public int? PokemonVarietyAsHiddenAbilityId { get; set; }
    }
}
