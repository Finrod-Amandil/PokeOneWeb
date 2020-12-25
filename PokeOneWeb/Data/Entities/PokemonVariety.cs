using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("PokemonVariety")]
    public class PokemonVariety
    {
        public int Id { get; set; }

        [Required]
        public string ResourceName { get; set; }

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

        [ForeignKey("PrimaryTypeId")]
        public ElementalType PrimaryType { get; set; }
        public int PrimaryTypeId { get; set; }

        [ForeignKey("SecondaryTypeId")]
        public ElementalType SecondaryType { get; set; }
        public int? SecondaryTypeId { get; set; }

        [ForeignKey("PrimaryAbilityId")]
        public Ability PrimaryAbility { get; set; }
        public int? PrimaryAbilityId { get; set; }

        [ForeignKey("SecondaryAbilityId")]
        public Ability SecondaryAbility { get; set; }
        public int? SecondaryAbilityId { get; set; }

        [ForeignKey("HiddenAbilityId")]
        public Ability HiddenAbility { get; set; }
        public int? HiddenAbilityId { get; set; }

        public List<PokemonHeldItem> HeldItems { get; set; } = new List<PokemonHeldItem>();
        public List<LearnableMove> LearnableMoves { get; set; } = new List<LearnableMove>();

        [ForeignKey("PvpTierId")]
        public PvpTier PvpTier { get; set; }
        public int? PvpTierId { get; set; }

        public bool DoInclude { get; set; }

        public bool IsFullyEvolved { get; set; }

        public bool IsMega { get; set; }

        public int Generation { get; set; }

        public int CatchRate { get; set; }

        public List<HuntingConfiguration> HuntingConfigurations { get; set; }

        public List<Build> Builds { get; set; }

        public string SmogonUrl { get; set; }

        public string BulbapediaUrl { get; set; }

        public string PokeOneCommunityUrl { get; set; }

        public string PokemonShowDownUrl { get; set; }

        public string SerebiiUrl { get; set; }

        public string PokemonDbUrl { get; set; }
    }
}
