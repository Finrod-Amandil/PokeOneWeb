using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Attributes;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Denotes a currency, that is used to pay on PokeOne, between players or between players and NPCs.
    /// Currencies may be PokeDollars, Gold, Reroll Tokens, Game Corner Coins, Heart Scales, Shards, ...
    /// </summary>
    [Table("Currency")]
    [Sheet("currencies")]
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

        // INDEXED
        [Required]
        public string Hash { get; set; }

        // INDEXED
        [Required]
        public string IdHash { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        public int? ItemId { get; set; }

        [NotMapped]
        public string ItemName { internal get; set; }

        public override string ToString()
        {
            return $"{Item?.ToString() ?? ItemName} (Currency)";
        }
    }
}