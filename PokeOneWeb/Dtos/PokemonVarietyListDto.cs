using System.Collections.Generic;

namespace PokeOneWeb.WebApi.Dtos
{
    public class PokemonVarietyListDto
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
        public int Bulk { get; set; }

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

        public List<PokemonVarietyUrlDto> Urls { get; set; }

        public string Notes { get; set; }
    }
}