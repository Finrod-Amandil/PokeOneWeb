using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// In-game time is divided into Morning, Day, Evening and Night. Depending
    /// on these phases, different Pokemon may spawn.
    /// </summary>
    [Table("TimeOfDay")]
    public class TimeOfDay : IHashedEntity
    {
        public static readonly string ANY = "Any";

        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<TimeOfDay>().HasIndexedHashes();
            builder.Entity<TimeOfDay>().HasIndex(tod => tod.Abbreviation).IsUnique();
            builder.Entity<TimeOfDay>().HasIndex(tod => tod.Name).IsUnique();

            builder.Entity<TimeOfDay>()
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

        // INDEXED, UNIQUE
        [Required]
        public string Name { get; set; }

        // INDEXED, UNIQUE
        [Required]
        public string Abbreviation { get; set; }

        public int SortIndex { get; set; }

        public string Color { get; set; }

        public List<SeasonTimeOfDay> SeasonTimes { get; set; } = new();

        public override string ToString()
        {
            return $"{Name} ({Abbreviation})";
        }
    }
}