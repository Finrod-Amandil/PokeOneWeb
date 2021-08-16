using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PokeOneWeb.Data.ReadModels.Interfaces;

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

        public string PrimaryType { get; set; }
        public string SecondaryType { get; set; }

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
