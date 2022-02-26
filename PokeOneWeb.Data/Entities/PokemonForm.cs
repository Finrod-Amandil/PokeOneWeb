using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    [Table("PokemonForm")]
    public class PokemonForm : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<PokemonForm>().HasIndexedHashes();
            builder.Entity<PokemonForm>().HasIndex(pf => pf.Name).IsUnique();

            builder.Entity<PokemonForm>()
                .HasOne(f => f.PokemonVariety)
                .WithMany(v => v.Forms)
                .HasForeignKey(f => f.PokemonVarietyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PokemonForm>()
                .HasOne(f => f.Availability)
                .WithMany()
                .HasForeignKey(f => f.AvailabilityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PokemonForm>()
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

        public string PokeApiName { get; set; }

        public int SortIndex { get; set; }

        public string SpriteName { get; set; }

        [ForeignKey("PokemonVarietyId")]
        public PokemonVariety PokemonVariety { get; set; }
        public int PokemonVarietyId { get; set; }

        [ForeignKey("AvailabilityId")]
        public PokemonAvailability Availability { get; set; }
        public int AvailabilityId { get; set; }

        public List<Spawn> PokemonSpawns { get; set; } = new();


        public override string ToString()
        {
            return Name;
        }
    }
}
