using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Denotes a currency, that is used to pay on PokéOne, between players or between player and NPC.
    /// Currencies may be PokéDollars, Gold, Reroll Tokens, Game Corner Coins, Heart Scales, Shards, ...
    /// </summary>
    [Table("Currency")]
    public class Currency : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<Currency>().HasIndexedHashes();

            builder.Entity<Currency>()
                .HasOne(c => c.Item)
                .WithMany()
                .HasForeignKey(c => c.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        [Key]
        public int Id { get; set; }

        //INDEXED
        [Required]
        public string Hash { get; set; }

        //INDEXED
        [Required]
        public string IdHash { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }
        public int? ItemId { get; set; }


        public override string ToString()
        {
            return $"{Item} (Currency)";
        }
    }
}
