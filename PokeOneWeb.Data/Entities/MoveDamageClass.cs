using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Attributes;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Moves are categorized into three damage classes: Physical, Special and Status.
    /// Physical moves deal damage based on the Attack and Defense stats of the involved Pokemon.
    /// Special moves deal damage based on the Special Attack and Special Defense stats of the involved Pokemon.
    /// Status moves do not deal direct damage.
    /// </summary>
    [Table("MoveDamageClass")]
    [Sheet("move_damage_classes")]
    public class MoveDamageClass : IHashedEntity, INamedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<MoveDamageClass>().HasIndexedHashes();
            builder.Entity<MoveDamageClass>().HasIndex(d => d.Name).IsUnique();

            builder.Entity<MoveDamageClass>()
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

        public override string ToString()
        {
            return Name;
        }
    }
}