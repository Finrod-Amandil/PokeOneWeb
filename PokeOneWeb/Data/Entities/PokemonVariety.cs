using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("PokemonVariety")]
    public class PokemonVariety
    {
        public int Id { get; set; }

        public string PokeApiName { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("PokemonSpeciesId")]
        public PokemonSpecies PokemonSpecies { get; set; }
        public int PokemonSpeciesId { get; set; }

        public List<PokemonForm> Forms { get; set; }
        [ForeignKey("DefaultFormId")]
        public PokemonForm DefaultForm { get; set; }
        public int? DefaultFormId { get; set; }

        [ForeignKey("BaseStatsId")]
        public Stats BaseStats { get; set; }
        public int? BaseStatsId { get; set; }

        [ForeignKey("EvYieldId")]
        public Stats EvYield { get; set; }
        public int? EvYieldId { get; set; }

        [ForeignKey("ElementalTypeCombinationId")]
        public ElementalTypeCombination ElementalTypeCombination { get; set; }
        public int? ElementalTypeCombinationId { get; set; }

        /// <summary>
        /// Every Pokémon species belongs to exactly one Evolution Chain. Species which do not evolve will
        /// belong to an Evolution Chain with no Evolutions.
        /// </summary>
        [ForeignKey("EvolutionChainId")]
        public EvolutionChain EvolutionChain { get; set; }
        public int EvolutionChainId { get; set; }

        [ForeignKey("PrimaryAbilityId")]
        public Ability PrimaryAbility { get; set; }
        public int? PrimaryAbilityId { get; set; }

        [ForeignKey("SecondaryAbilityId")]
        public Ability SecondaryAbility { get; set; }
        public int? SecondaryAbilityId { get; set; }

        [ForeignKey("HiddenAbilityId")]
        public Ability HiddenAbility { get; set; }
        public int? HiddenAbilityId { get; set; }

        public bool DoInclude { get; set; }

        public List<PokemonHeldItem> HeldItems { get; set; }
        public List<LearnableMove> LearnableMoves { get; set; }
    }
}
