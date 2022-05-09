using System.Collections.Generic;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    public class PokemonVarietyNameReadModel : IReadModel
    {
        public string Name { get; set; }
        public string ResourceName { get; set; }
    }

    public class PokemonVarietyListReadModel : PokemonVarietyNameReadModel
    {
        public int SortIndex { get; set; }
        public int PokedexNumber { get; set; }
        public string SpriteName { get; set; }

        public string PrimaryElementalType { get; set; }
        public string SecondaryElementalType { get; set; }

        public int Attack { get; set; }
        public int SpecialAttack { get; set; }
        public int Defense { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public int HitPoints { get; set; }
        public int StatTotal { get; set; }
        public int Bulk { get; set; }

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

        public List<PokemonVarietyUrlReadModel> Urls { get; set; } = new();
    }

    public class PokemonVarietyReadModel : PokemonVarietyListReadModel
    {
        public string PreviousPokemonResourceName { get; set; }
        public string PreviousPokemonSpriteName { get; set; }
        public string PreviousPokemonName { get; set; }
        public string NextPokemonResourceName { get; set; }
        public string NextPokemonSpriteName { get; set; }
        public string NextPokemonName { get; set; }

        public int CatchRate { get; set; }
        public bool HasGender { get; set; }
        public decimal MaleRatio { get; set; }
        public decimal FemaleRatio { get; set; }
        public int EggCycles { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public int ExpYield { get; set; }

        public int AttackEv { get; set; }
        public int SpecialAttackEv { get; set; }
        public int DefenseEv { get; set; }
        public int SpecialDefenseEv { get; set; }
        public int SpeedEv { get; set; }
        public int HitPointsEv { get; set; }

        public decimal PrimaryAbilityAttackBoost { get; set; }
        public decimal PrimaryAbilitySpecialAttackBoost { get; set; }
        public decimal PrimaryAbilityDefenseBoost { get; set; }
        public decimal PrimaryAbilitySpecialDefenseBoost { get; set; }
        public decimal PrimaryAbilitySpeedBoost { get; set; }
        public string PrimaryAbilityBoostConditions { get; set; }

        public decimal SecondaryAbilityAttackBoost { get; set; }
        public decimal SecondaryAbilitySpecialAttackBoost { get; set; }
        public decimal SecondaryAbilityDefenseBoost { get; set; }
        public decimal SecondaryAbilitySpecialDefenseBoost { get; set; }
        public decimal SecondaryAbilitySpeedBoost { get; set; }
        public string SecondaryAbilityBoostConditions { get; set; }

        public decimal HiddenAbilityAttackBoost { get; set; }
        public decimal HiddenAbilitySpecialAttackBoost { get; set; }
        public decimal HiddenAbilityDefenseBoost { get; set; }
        public decimal HiddenAbilitySpecialDefenseBoost { get; set; }
        public decimal HiddenAbilitySpeedBoost { get; set; }
        public string HiddenAbilityBoostConditions { get; set; }

        public string Notes { get; set; }

        public List<PokemonFormReadModel> Forms { get; set; } = new();
        public List<EvolutionReadModel> Evolutions { get; set; } = new();
        public List<EvolutionAbilityReadModel> PrimaryEvolutionAbilities { get; set; } = new();
        public List<EvolutionAbilityReadModel> SecondaryEvolutionAbilities { get; set; } = new();
        public List<EvolutionAbilityReadModel> HiddenEvolutionAbilities { get; set; } = new();
        public List<AttackEffectivityReadModel> DefenseAttackEffectivities { get; set; } = new();
        public List<SpawnReadModel> Spawns { get; set; } = new();
        public List<LearnableMoveReadModel> LearnableMoves { get; set; } = new();
    }
}