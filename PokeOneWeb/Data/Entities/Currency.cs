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

        /// <summary>
        /// Only used, if Item is unused.
        /// </summary>
        public int Name { get; set; }

        /// <summary>
        /// Used, if the currency is an Item.
        /// </summary>
        [ForeignKey("ItemId")]
        public Item Item { get; set; }
        public int? ItemId { get; set; }
    }
}
