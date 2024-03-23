using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Attributes;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// A spawn denotes that a Pokemon Form can appear in a certain location.
    /// Depending on how the spawn occurs, it has a different spawn type, i.e.
    /// whether the Pokemon can be encountered in tall grass, on water, through
    /// fishing or as a boss reward. The spawn can occur during various seasons
    /// and times of day, which is modeled with one or more spawn opportunities.
    ///
    /// If a spawn occurs with different rarities during different times, this is
    /// modeled with multiple spawn instances with one spawn opportunity each.
    /// </summary>
    [Table("Spawn")]
    [Sheet("spawns")]
    public class Spawn : IHashedEntity
    {
        public static readonly string UnknownCommonality = "?";

        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<Spawn>().HasIndexedHashes();

            builder.Entity<Spawn>()
                .HasOne(x => x.ImportSheet)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<Spawn>()
                .HasOne(s => s.SpawnType)
                .WithMany()
                .HasForeignKey(s => s.SpawnTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Spawn>()
                .HasOne(s => s.PokemonForm)
                .WithMany(pf => pf.PokemonSpawns)
                .HasForeignKey(s => s.PokemonFormId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Spawn>()
                .HasOne(s => s.Location)
                .WithMany(l => l.PokemonSpawns)
                .HasForeignKey(s => s.LocationId)
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

        [ForeignKey("ImportSheetId")]
        public ImportSheet ImportSheet { get; set; }

        public int ImportSheetId { get; set; }

        /// <summary>
        /// Gets or sets definition of rarity using predicates to describe
        /// perceived (not measured) rarities, i.e. "Common" or "Rare".
        /// </summary>
        public string SpawnCommonality { get; set; }

        /// <summary>
        /// Gets or sets definition of rarity as percentage (ground truth or measured).
        /// </summary>
        [Column(TypeName = "decimal(18,4)")]
        public decimal? SpawnProbability { get; set; }

        /// <summary>
        /// Gets or sets definition of rarity based on counted encounters. This
        /// measure needs to be compared to other spawns in the same place
        /// in order to determine a percentage for the rarity.
        /// </summary>
        public int? EncounterCount { get; set; }

        public bool IsConfirmed { get; set; }

        public int LowestLevel { get; set; }

        public int HighestLevel { get; set; }

        public string Notes { get; set; }

        [ForeignKey("SpawnTypeId")]
        public SpawnType SpawnType { get; set; }

        public int SpawnTypeId { get; set; }

        [NotMapped]
        public string SpawnTypeName { internal get; set; }

        [ForeignKey("PokemonFormId")]
        public PokemonForm PokemonForm { get; set; }

        public int PokemonFormId { get; set; }

        [NotMapped]
        public string PokemonFormName { internal get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        public int LocationId { get; set; }

        [NotMapped]
        public string LocationName { internal get; set; }

        public List<SpawnOpportunity> SpawnOpportunities { get; set; } = new();

        public override string ToString()
        {
            return $"{PokemonForm?.ToString() ?? PokemonFormName} in " +
                   $"{Location?.ToString() ?? LocationName}, type " +
                   $"{SpawnType?.ToString() ?? SpawnTypeName}";
        }
    }
}