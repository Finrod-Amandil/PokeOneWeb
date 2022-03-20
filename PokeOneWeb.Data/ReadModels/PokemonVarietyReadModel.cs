using PokeOneWeb.Data.ReadModels.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("PokemonVarietyReadModel")]
    public class PokemonVarietyReadModel : IReadModel
    {
        public int Id { get; set; }

        [Required]
        public int ApplicationDbId { get; set; }

        public string ResourceName { get; set; }

        public int SortIndex { get; set; }

        public int PokedexNumber { get; set; }

        public string Name { get; set; }

        public string SpriteName { get; set; }

        public string PrimaryElementalType { get; set; }
        public string SecondaryElementalType { get; set; }

        public int Attack { get; set; }
        public int SpecialAttack { get; set; }
        public int Defense { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public int HitPoints { get; set; }

        public string PrimaryAbility { get; set; }
        public string PrimaryAbilityEffect { get; set; }
        public string SecondaryAbility { get; set; }
        public string SecondaryAbilityEffect { get; set; }
        public string HiddenAbility { get; set; }
        public string HiddenAbilityEffect { get; set; }

        public string Availability { get; set; }
        public string AvailabilityDescription { get; set; }

        public string PvpTier { get; set; }
        public int PvpTierSortIndex { get; set; }

        public int Generation { get; set; }
        public bool IsFullyEvolved { get; set; }
        public bool IsMega { get; set; }
        public int CatchRate { get; set; }
        public bool HasGender { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal MaleRatio { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal FemaleRatio { get; set; }
        public int EggCycles { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal Height { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal Weight { get; set; }
        public int ExpYield { get; set; }

        public int AttackEv { get; set; }
        public int SpecialAttackEv { get; set; }
        public int DefenseEv { get; set; }
        public int SpecialDefenseEv { get; set; }
        public int SpeedEv { get; set; }
        public int HitPointsEv { get; set; }

        public string Notes { get; set; }

        public string PreviousPokemonResourceName { get; set; }
        public string PreviousPokemonSpriteName { get; set; }
        public string PreviousPokemonName { get; set; }
        public string NextPokemonResourceName { get; set; }
        public string NextPokemonSpriteName { get; set; }
        public string NextPokemonName { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal PrimaryAbilityAttackBoost { get; set; }
        [Column(TypeName = "decimal(4,1)")]
        public decimal PrimaryAbilitySpecialAttackBoost { get; set; }
        [Column(TypeName = "decimal(4,1)")]
        public decimal PrimaryAbilityDefenseBoost { get; set; }
        [Column(TypeName = "decimal(4,1)")]
        public decimal PrimaryAbilitySpecialDefenseBoost { get; set; }
        [Column(TypeName = "decimal(4,1)")]
        public decimal PrimaryAbilitySpeedBoost { get; set; }
        [Column(TypeName = "decimal(4,1)")]
        public decimal PrimaryAbilityHitPointsBoost { get; set; }
        public string PrimaryAbilityBoostConditions { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal SecondaryAbilityAttackBoost { get; set; }
        [Column(TypeName = "decimal(4,1)")]
        public decimal SecondaryAbilitySpecialAttackBoost { get; set; }
        [Column(TypeName = "decimal(4,1)")]
        public decimal SecondaryAbilityDefenseBoost { get; set; }
        [Column(TypeName = "decimal(4,1)")]
        public decimal SecondaryAbilitySpecialDefenseBoost { get; set; }
        [Column(TypeName = "decimal(4,1)")]
        public decimal SecondaryAbilitySpeedBoost { get; set; }
        [Column(TypeName = "decimal(4,1)")]
        public decimal SecondaryAbilityHitPointsBoost { get; set; }
        public string SecondaryAbilityBoostConditions { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal HiddenAbilityAttackBoost { get; set; }
        [Column(TypeName = "decimal(4,1)")]
        public decimal HiddenAbilitySpecialAttackBoost { get; set; }
        [Column(TypeName = "decimal(4,1)")]
        public decimal HiddenAbilityDefenseBoost { get; set; }
        [Column(TypeName = "decimal(4,1)")]
        public decimal HiddenAbilitySpecialDefenseBoost { get; set; }
        [Column(TypeName = "decimal(4,1)")]
        public decimal HiddenAbilitySpeedBoost { get; set; }
        [Column(TypeName = "decimal(4,1)")]
        public decimal HiddenAbilityHitPointsBoost { get; set; }
        public string HiddenAbilityBoostConditions { get; set; }

        public List<PokemonVarietyVarietyReadModel> Varieties { get; set; } = new();
        public List<PokemonVarietyFormReadModel> Forms { get; set; } = new();
        public List<PokemonVarietyUrlReadModel> Urls { get; set; } = new();
        public List<EvolutionAbilityReadModel> PrimaryEvolutionAbilities { get; set; } = new();
        public List<EvolutionAbilityReadModel> SecondaryEvolutionAbilities { get; set; } = new();
        public List<EvolutionAbilityReadModel> HiddenEvolutionAbilities { get; set; } = new();
        public List<AttackEffectivityReadModel> DefenseAttackEffectivities { get; set; } = new();
        public List<SpawnReadModel> Spawns { get; set; } = new();
        public List<EvolutionReadModel> Evolutions { get; set; } = new();
        public List<LearnableMoveReadModel> LearnableMoves { get; set; } = new();
        public List<HuntingConfigurationReadModel> HuntingConfigurations { get; set; } = new();
        public List<BuildReadModel> Builds { get; set; } = new();
    }
}
