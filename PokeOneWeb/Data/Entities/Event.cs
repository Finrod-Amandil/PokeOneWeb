using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Data.Entities
{
    [Table("Event")]
    public class Event : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<Event>().HasIndexedHashes();
            builder.Entity<Event>().HasIndex(e => e.Name).IsUnique();

            builder.Entity<Event>()
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

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
