using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// A Pokemon Species is a kind of Pokemon with the granularity as applied to the in-game
    /// Pokedex. Pokemon Species may appear in one or more varieties, modeled with PokemonVarieties.
    /// </summary>
    [Table("PokemonSpecies")]
    public class PokemonSpecies
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<PokemonSpecies>().HasIndex(ps => ps.Name).IsUnique();
            builder.Entity<PokemonSpecies>().HasIndex(ps => ps.PokedexNumber).IsUnique();

            builder.Entity<PokemonSpecies>()
                .HasOne(p => p.DefaultVariety)
                .WithMany()
                .HasForeignKey(p => p.DefaultVarietyId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

        [Key]
        public int Id { get; set; }

        //INDEXED, UNIQUE
        [Required]
        public int PokedexNumber { get; set; }

        //INDEXED, UNIQUE
        [Required]
        public string Name { get; set; }

        public string PokeApiName { get; set; }

        [ForeignKey("DefaultVarietyId")]
        public PokemonVariety DefaultVariety { get; set; }
        public int? DefaultVarietyId { get; set; }

        public List<PokemonVariety> Varieties { get; set; }
        public List<Evolution> Evolutions { get; set; }


        public override string ToString()
        {
            return $"{PokedexNumber} {Name}";
        }
    }
}
