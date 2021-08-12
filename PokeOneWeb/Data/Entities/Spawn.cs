using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Data.Entities
{
    [Table("Spawn")]
    public class Spawn : IHashedEntity
    {
        public static readonly string UNKNOWN_COMMONALITY = "?";

        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<Spawn>().HasIndexedHashes();

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

            builder.Entity<Spawn>()
                .HasOne(x => x.ImportSheet)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);
        }

        [Key]
        public int Id { get; set; }

        //INDEXED
        [Required]
        public string Hash { get; set; }

        //INDEXED
        [Required]
        public string IdHash { get; set; }

        [ForeignKey("ImportSheetId")]
        public ImportSheet ImportSheet { get; set; }
        public int ImportSheetId { get; set; }

        public string SpawnCommonality { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? SpawnProbability { get; set; }

        public int? EncounterCount { get; set; }

        public bool IsConfirmed { get; set; }

        public int LowestLevel { get; set; }

        public int HighestLevel { get; set; }

        public string Notes { get; set; }

        [ForeignKey("SpawnTypeId")]
        public SpawnType SpawnType { get; set; }
        public int SpawnTypeId { get; set; }

        [ForeignKey("PokemonFormId")]
        public PokemonForm PokemonForm { get; set; }
        public int PokemonFormId { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public int LocationId { get; set; }

        public List<SpawnOpportunity> SpawnOpportunities { get; set; } = new List<SpawnOpportunity>();


        public override string ToString()
        {
            return $"{PokemonForm} in {Location}, type {SpawnType}";
        }
    }
}
