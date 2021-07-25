using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Data.Entities
{
    [Table("HuntingConfiguration")]
    public class HuntingConfiguration : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<HuntingConfiguration>().HasIndexedHashes();

            builder.Entity<HuntingConfiguration>()
                .HasOne(hc => hc.PokemonVariety)
                .WithMany(pv => pv.HuntingConfigurations)
                .HasForeignKey(hc => hc.PokemonVarietyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<HuntingConfiguration>()
                .HasOne(hc => hc.Nature)
                .WithMany()
                .HasForeignKey(hc => hc.NatureId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<HuntingConfiguration>()
                .HasOne(hc => hc.Ability)
                .WithMany()
                .HasForeignKey(hc => hc.AbilityId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        [Key]
        public int Id { get; set; }

        //INDEXED
        [Required]
        public string Hash { get; set; }

        //INDEXED
        [Required]
        public string IdHash { get; set; }

        [ForeignKey("PokemonVarietyId")]
        public PokemonVariety PokemonVariety { get; set; }
        public int PokemonVarietyId { get; set; }

        [ForeignKey("NatureId")]
        public Nature Nature { get; set; }
        public int NatureId { get; set; }

        [ForeignKey("AbilityId")]
        public Ability Ability { get; set; }
        public int AbilityId { get; set; }

        public override string ToString()
        {
            return $"{Nature} {Ability} {PokemonVariety}";
        }
    }
}
