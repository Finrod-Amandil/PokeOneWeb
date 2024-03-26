using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    public class ItemStatBoostPokemonReadModel : IReadModel
    {
        public string ItemName { get; set; }
        public string ItemResourceName { get; set; }

        public string ItemEffect { get; set; }

        public decimal AttackBoost { get; set; }
        public decimal DefenseBoost { get; set; }
        public decimal SpecialAttackBoost { get; set; }
        public decimal SpecialDefenseBoost { get; set; }
        public decimal SpeedBoost { get; set; }
        public decimal HitPointsBoost { get; set; }

        public bool HasRequiredPokemon { get; set; }
        public string RequiredPokemonResourceName { get; set; }
        public string RequiredPokemonName { get; set; }
    }
}