using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("PokemonReadModel")]
    public class PokemonReadModel
    {
        public int Id { get; set; }

        public string ResourceName { get; set; }

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
    }
}
