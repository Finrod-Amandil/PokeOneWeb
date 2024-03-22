using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Attributes;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Whether / under what conditions a Pokemon can generally be obtained.
    /// </summary>
    [Table("PokemonAvailability")]
    [Sheet("availabilities")]
    public class PokemonAvailability : IHashedEntity, INamedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<PokemonAvailability>().HasIndexedHashes();
            builder.Entity<PokemonAvailability>().HasIndex(a => a.Name).IsUnique();
        }

        [Key]
        public int Id { get; set; }

        // INDEXED
        [Required]
        public string Hash { get; set; }

        // INDEXED
        [Required]
        public string IdHash { get; set; }

        // INDEXED, UNIQUE
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}