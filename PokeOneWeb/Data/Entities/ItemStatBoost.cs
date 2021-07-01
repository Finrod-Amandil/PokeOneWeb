using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("ItemStatBoost")]
    public class ItemStatBoost
    {
        public int Id { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }
        public int ItemId { get; set; }

        [ForeignKey("StatBoostId")]
        public Stats StatBoost { get; set; }
        public int StatBoostId { get; set; }

        public List<ItemStatBoostPokemon> RequiredPokemon { get; set; }
    }
}