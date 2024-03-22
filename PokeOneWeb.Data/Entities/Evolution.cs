using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Attributes;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Certain Pokemon Varieties are capable of evolving into a usually stronger, other Pokemon Variety.
    /// Evolutions are only possible when certain conditions are met (Evolution trigger). Depending on the
    /// conditions, the Pokemon Variety may evolve into one of multiple different evolved forms.
    /// Some evolutions are only temporary and / or reversible. If the conditions for the evolution
    /// are impossible to meet, the evolution is considered unavailable.
    /// </summary>
    [Table("Evolution")]
    [Sheet("evolutions")]
    public class Evolution : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<Evolution>().HasIndexedHashes();

            builder.Entity<Evolution>()
                .HasOne(e => e.BasePokemonSpecies)
                .WithMany(s => s.Evolutions)
                .HasForeignKey(e => e.BasePokemonSpeciesId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Evolution>()
                .HasOne(e => e.BasePokemonVariety)
                .WithMany()
                .HasForeignKey(e => e.BasePokemonVarietyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Evolution>()
                .HasOne(e => e.EvolvedPokemonVariety)
                .WithMany()
                .HasForeignKey(e => e.EvolvedPokemonVarietyId)
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

        /// <summary>
        /// Gets or sets under which conditions the evolution is possible.
        /// </summary>
        [Required]
        public string EvolutionTrigger { get; set; }

        /// <summary>
        /// Gets or sets the position of the unevolved Pokemon in a chain of evolutions.
        /// May be between 0 and 2 (inclusive).
        /// </summary>
        public int BaseStage { get; set; }

        /// <summary>
        /// Gets or sets the position of the evolved Pokemon in a chain of evolutions.
        /// May be between 1 and 3 (inclusive).
        /// </summary>
        public int EvolvedStage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether whether the evolution is permanent or can be reversed.
        /// </summary>
        public bool IsReversible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether whether it is possible to trigger this evolution.
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether whether the evolution should be displayed or omitted.
        /// </summary>
        public bool DoInclude { get; set; }

        /// <summary>
        /// Gets or sets the Pokemon Species of the Pokemon Variety, which forms the start of
        /// the evolution chain. For the evolution of Ivysaur to Venusaur, Bulbasaur would
        /// be the base species of the chain.
        /// </summary>
        [ForeignKey("BasePokemonSpeciesId")]
        public PokemonSpecies BasePokemonSpecies { get; set; }

        public int BasePokemonSpeciesId { get; set; }

        [NotMapped]
        public string BasePokemonSpeciesName { internal get; set; }

        /// <summary>
        /// Gets or sets the Pokemon Variety before the evolution took place.
        /// </summary>
        [ForeignKey("BasePokemonVarietyId")]
        public PokemonVariety BasePokemonVariety { get; set; }

        public int BasePokemonVarietyId { get; set; }

        [NotMapped]
        public string BasePokemonVarietyName { internal get; set; }

        /// <summary>
        /// Gets or sets the Pokemon Variety after the evolution took place.
        /// </summary>
        [ForeignKey("EvolvedPokemonVarietyId")]
        public PokemonVariety EvolvedPokemonVariety { get; set; }

        public int EvolvedPokemonVarietyId { get; set; }

        [NotMapped]
        public string EvolvedPokemonVarietyName { internal get; set; }

        public override string ToString()
        {
            return $"{BasePokemonVariety?.ToString() ?? BasePokemonVarietyName} -> " +
                   $"{EvolvedPokemonVariety?.ToString() ?? EvolvedPokemonVarietyName}";
        }
    }
}