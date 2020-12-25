using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("Evolution")]
    public class Evolution
    {
        public int Id { get; set; }

        [ForeignKey("BasePokemonSpeciesId")]
        public PokemonSpecies BasePokemonSpecies { get; set; }
        public int BasePokemonSpeciesId { get; set; }

        [ForeignKey("BasePokemonVarietyId")]
        public PokemonVariety BasePokemonVariety { get; set; }
        public int BasePokemonVarietyId { get; set; }

        [ForeignKey("EvolvedPokemonVarietyId")]
        public PokemonVariety EvolvedPokemonVariety { get; set; }
        public int EvolvedPokemonVarietyId { get; set; }

        public string EvolutionTrigger { get; set; }

        public int BaseStage { get; set; }

        public int EvolvedStage { get; set; }

        public bool IsReversible { get; set; }

        public bool IsAvailable { get; set; }

        public bool DoInclude { get; set; }
    }
}
