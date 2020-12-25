using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Denotes a currency, that is used to pay on PokéOne, between players or between player and NPC.
    /// Currencies may be PokéDollars, Gold, Reroll Tokens, Game Corner Coins, Heart Scales, Shards, ...
    /// </summary>
    [Table("Currency")]
    public class Currency
    {
        public int Id { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }
        public int? ItemId { get; set; }
    }
}
