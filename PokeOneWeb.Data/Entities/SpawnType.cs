using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Attributes;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Categorization of spawns. Usually all spawns with the same spawn type in the same
    /// location are encountered simultaneously.
    /// </summary>
    [Table("SpawnType")]
    [Sheet("spawn_types")]
    public class SpawnType : IHashedEntity, INamedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<SpawnType>().HasIndexedHashes();
            builder.Entity<SpawnType>().HasIndex(st => st.Name).IsUnique();

            builder.Entity<SpawnType>()
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

        public int SortIndex { get; set; }

        public bool IsSyncable { get; set; }

        public bool IsInfinite { get; set; }

        public string Color { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}