using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Denotes, that the given item stat boost is only available for a specific Pokemon Variety.
    /// </summary>
    [Table("ItemStatBoostPokemon")]
    public class ItemStatBoostPokemon : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<ItemStatBoostPokemon>().HasIndexedHashes();

            builder.Entity<ItemStatBoostPokemon>()
                .HasOne(isbp => isbp.ItemStatBoost)
                .WithMany(isb => isb.RequiredPokemon)
                .HasForeignKey(isbp => isbp.ItemStatBoostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ItemStatBoostPokemon>()
                .HasOne(isbp => isbp.PokemonVariety)
                .WithMany()
                .HasForeignKey(isbp => isbp.PokemonVarietyId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<ItemStatBoostPokemon>()
                .HasOne(x => x.ImportSheet)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);
        }

        [Key]
        public int Id { get; set; }

        // INDEXED
        [Required]
        public string Hash { get; set; }

        // INDEXED
        [Required]
        public string IdHash { get; set; }

        [ForeignKey("ImportSheetId")]
        public ImportSheet ImportSheet { get; set; }

        public int ImportSheetId { get; set; }

        [ForeignKey("ItemStatBoostId")]
        public ItemStatBoost ItemStatBoost { get; set; }

        public int ItemStatBoostId { get; set; }

        [ForeignKey("PokemonVarietyId")]
        public PokemonVariety PokemonVariety { get; set; }

        public int? PokemonVarietyId { get; set; } // Is null, if no Pokemon is req.

        public override string ToString()
        {
            return $"{ItemStatBoost} for {PokemonVariety}";
        }
    }
}