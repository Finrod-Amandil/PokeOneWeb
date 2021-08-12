using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Data.Entities
{
    [Table("Season")]
    public class Season : IHashedEntity
    {
        public static readonly string ANY = "Any";

        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<Season>().HasIndexedHashes();
            builder.Entity<Season>().HasIndex(s => s.Abbreviation).IsUnique();
            builder.Entity<Season>().HasIndex(s => s.Name).IsUnique();

            builder.Entity<Season>()
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

        //INDEXED, UNIQUE
        [Required]
        public string Name { get; set; }

        //INDEXED, UNIQUE
        [Required]
        public string Abbreviation { get; set; }

        public int SortIndex { get; set; }

        public string Color { get; set; }


        public override string ToString()
        {
            return $"{Name} ({Abbreviation})";
        }
    }
}
