using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Data.Entities
{
    [Table("Evolution")]
    public class Evolution : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<Evolution>().HasIndexedHashes();

            builder.Entity<Evolution>()
                .HasOne(e => e.BasePokemonSpecies)
                .WithMany(s => s.Evolutions)
                .HasForeignKey(e => e.BasePokemonSpeciesId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Evolution>()
                .HasOne(e => e.BasePokemonVariety)
                .WithMany()
                .HasForeignKey(e => e.BasePokemonVarietyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Evolution>()
                .HasOne(e => e.EvolvedPokemonVariety)
                .WithMany()
                .HasForeignKey(e => e.EvolvedPokemonVarietyId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<Evolution>()
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

        [Required]
        public string EvolutionTrigger { get; set; }

        public int BaseStage { get; set; }

        public int EvolvedStage { get; set; }

        public bool IsReversible { get; set; }

        public bool IsAvailable { get; set; }

        public bool DoInclude { get; set; }

        [ForeignKey("BasePokemonSpeciesId")]
        public PokemonSpecies BasePokemonSpecies { get; set; }
        public int BasePokemonSpeciesId { get; set; }

        [ForeignKey("BasePokemonVarietyId")]
        public PokemonVariety BasePokemonVariety { get; set; }
        public int BasePokemonVarietyId { get; set; }

        [ForeignKey("EvolvedPokemonVarietyId")]
        public PokemonVariety EvolvedPokemonVariety { get; set; }
        public int EvolvedPokemonVarietyId { get; set; }


        public override string ToString()
        {
            return $"{BasePokemonVariety} --> {EvolvedPokemonVariety}";
        }
    }
}
