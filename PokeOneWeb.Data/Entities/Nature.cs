using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Attributes;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Every Pokemon has a Nature. The nature of a Pokemon increases one stat
    /// (Attack, Special Attack, Defense, Special Defense, Speed) and decreases
    /// a stat by 10%. Depending on the Pokemon Variety, some natures are
    /// beneficial while others are detrimental. Natures which boost and decrease
    /// the same nature and thus have no effect, are called "neutral" natures.
    /// The HP stat is unaffected by natures.
    /// </summary>
    [Table("Nature")]
    [Sheet("natures")]
    public class Nature : IHashedEntity, INamedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<Nature>().HasIndexedHashes();
            builder.Entity<Nature>().HasIndex(n => n.Name).IsUnique();

            builder.Entity<Nature>()
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

        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}