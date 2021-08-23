using System.Collections.Generic;

namespace PokeOneWeb.Dtos
{
    public class PokemonVarietyDto
    {
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
        public int StatTotal { get; set; }
        public string PrimaryAbility { get; set; }
        public string PrimaryAbilityEffect { get; set; }
        public string SecondaryAbility { get; set; }
        public string SecondaryAbilityEffect { get; set; }
        public string HiddenAbility { get; set; }
        public string HiddenAbilityEffect { get; set; }
        public string Availability { get; set; }
        public string PvpTier { get; set; }
        public int PvpTierSortIndex { get; set; }
        public int Generation { get; set; }
        public bool IsFullyEvolved { get; set; }
        public bool IsMega { get; set; }
        public int CatchRate { get; set; }
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

        public decimal PrimaryAbilityAttackBoost { get; set; }
        public decimal PrimaryAbilitySpecialAttackBoost { get; set; }
        public decimal PrimaryAbilityDefenseBoost { get; set; }
        public decimal PrimaryAbilitySpecialDefenseBoost { get; set; }
        public decimal PrimaryAbilitySpeedBoost { get; set; }
        public decimal PrimaryAbilityHitPointsBoost { get; set; }
        public string PrimaryAbilityBoostConditions { get; set; }

        public decimal SecondaryAbilityAttackBoost { get; set; }
        public decimal SecondaryAbilitySpecialAttackBoost { get; set; }
        public decimal SecondaryAbilityDefenseBoost { get; set; }
        public decimal SecondaryAbilitySpecialDefenseBoost { get; set; }
        public decimal SecondaryAbilitySpeedBoost { get; set; }
        public decimal SecondaryAbilityHitPointsBoost { get; set; }
        public string SecondaryAbilityBoostConditions { get; set; }

        public decimal HiddenAbilityAttackBoost { get; set; }
        public decimal HiddenAbilitySpecialAttackBoost { get; set; }
        public decimal HiddenAbilityDefenseBoost { get; set; }
        public decimal HiddenAbilitySpecialDefenseBoost { get; set; }
        public decimal HiddenAbilitySpeedBoost { get; set; }
        public decimal HiddenAbilityHitPointsBoost { get; set; }
        public string HiddenAbilityBoostConditions { get; set; }

        public IEnumerable<PokemonVarietyUrlDto> Urls { get; set; }
        public IEnumerable<EvolutionAbilityDto> PrimaryEvolutionAbilities { get; set; }
        public IEnumerable<EvolutionAbilityDto> SecondaryEvolutionAbilities { get; set; }
        public IEnumerable<EvolutionAbilityDto> HiddenEvolutionAbilities { get; set; }
        public IEnumerable<AttackEffectivityDto> DefenseAttackEffectivities { get; set; }
        public IEnumerable<SpawnDto> Spawns { get; set; }
        public IEnumerable<EvolutionDto> Evolutions { get; set; }
        public IEnumerable<LearnableMoveDto> LearnableMoves { get; set; }
        public IEnumerable<HuntingConfigurationDto> HuntingConfigurations { get; set; }
        public IEnumerable<BuildDto> Builds { get; set; }
    }
}
