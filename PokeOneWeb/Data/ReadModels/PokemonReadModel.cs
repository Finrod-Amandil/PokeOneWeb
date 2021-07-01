using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("PokemonReadModel")]
    public class PokemonReadModel
    {
        public int Id { get; set; }

        public string ResourceName { get; set; }

        public int SortIndex { get; set; }

        public int PokedexNumber { get; set; }

        public string Name { get; set; }

        public string SpriteName { get; set; }

        public string Type1 { get; set; }
        public string Type2 { get; set; }

        public int Atk { get; set; }
        public int Spa { get; set; }
        public int Def { get; set; }
        public int Spd { get; set; }
        public int Spe { get; set; }
        public int Hp { get; set; }
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

        public string SmogonUrl { get; set; }
        public string BulbapediaUrl { get; set; }
        public string PokeOneCommunityUrl { get; set; }
        public string PokemonShowDownUrl { get; set; }
        public string SerebiiUrl { get; set; }
        public string PokemonDbUrl { get; set; }

        public List<AbilityTurnsIntoReadModel> PrimaryAbilityTurnsInto { get; set; }
        public List<AbilityTurnsIntoReadModel> SecondaryAbilityTurnsInto { get; set; }
        public List<AbilityTurnsIntoReadModel> HiddenAbilityTurnsInto { get; set; }
        public int CatchRate { get; set; }
        public List<HuntingConfigurationReadModel> HuntingConfigurations { get; set; }
        public int AtkEv { get; set; }
        public int SpaEv { get; set; }
        public int DefEv { get; set; }
        public int SpdEv { get; set; }
        public int SpeEv { get; set; }
        public int HpEv { get; set; }
        public List<AttackEffectivityReadModel> DefenseAttackEffectivities { get; set; }
        public List<SpawnReadModel> Spawns { get; set; }
        public List<EvolutionReadModel> Evolutions { get; set; }
        public List<LearnableMoveReadModel> LearnableMoves { get; set; }
        public List<BuildReadModel> Builds { get; set; }
        public string Notes { get; set; }
    }
}
