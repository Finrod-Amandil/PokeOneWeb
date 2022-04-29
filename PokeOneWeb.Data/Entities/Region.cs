using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// The Pokemon World is divided into major areas called Regions.
    /// </summary>
    [Table("Region")]
    public class Region : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<Region>().HasIndexedHashes();
            builder.Entity<Region>().HasIndex(r => r.Name).IsUnique();
            builder.Entity<Region>().HasIndex(r => r.ResourceName).IsUnique();

            builder.Entity<Region>()
                .HasOne(r => r.Event)
                .WithMany()
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Region>()
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

        public string ResourceName { get; set; }

        public bool IsEventRegion { get; set; }

        public string Color { get; set; }
        public string Description { get; set; }
        public bool IsReleased { get; set; }
        public bool IsMainRegion { get; set; }
        public bool IsSideRegion { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; }

        public int? EventId { get; set; }

        public List<LocationGroup> LocationGroups { get; set; } = new();

        public override string ToString()
        {
            return Name;
        }
    }
}