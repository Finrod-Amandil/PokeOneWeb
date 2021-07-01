using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("ItemStatBoostPokemon")]
    public class ItemStatBoostPokemon
    {
        public int Id { get; set; }

        [ForeignKey("ItemStatBoostId")]
        public ItemStatBoost ItemStatBoost { get; set; }
        public int ItemStatBoostId { get; set; }

        [ForeignKey("PokemonVarietyId")]
        public PokemonVariety PokemonVariety { get; set; }
        public int PokemonVarietyId { get; set; }
    }
}