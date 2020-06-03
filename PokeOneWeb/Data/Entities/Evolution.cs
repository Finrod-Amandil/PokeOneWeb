using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("Evolution")]
    public class Evolution
    {
        public int Id { get; set; }

        [ForeignKey("EvolutionChainId")]
        public EvolutionChain EvolutionChain { get; set; }
        public int EvolutionChainId { get; set; }

        [ForeignKey("BasePokemonVarietyId")]
        public PokemonVariety BasePokemonVariety { get; set; }
        public int BasePokemonVarietyId { get; set; }

        [ForeignKey("EvolvedPokemonVarietyId")]
        public PokemonVariety EvolvedPokemonVariety { get; set; }
        public int EvolvedPokemonVarietyId { get; set; }

        [Required]
        public string EvolutionTrigger { get; set; }
    }
}
