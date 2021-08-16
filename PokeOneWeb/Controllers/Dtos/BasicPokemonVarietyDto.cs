using System.Collections.Generic;

namespace PokeOneWeb.Controllers.Dtos
{
    public class BasicPokemonVarietyDto
    {
        public int Id { get; set; }

        public string ResourceName { get; set; }

        public int SortIndex { get; set; }

        public int PokedexNumber { get; set; }

        public string Name { get; set; }

        public string SpriteName { get; set; }

        public string PrimaryElementalType { get; set; }
        public string SecondaryElementalType { get; set; }

        public int Atk { get; set; }
        public int Spa { get; set; }
        public int Def { get; set; }
        public int Spd { get; set; }
        public int Spe { get; set; }
        public int Hp { get; set; }

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

