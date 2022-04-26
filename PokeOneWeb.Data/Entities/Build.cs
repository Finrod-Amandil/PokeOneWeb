using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Attributes;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// A build represents a possible, recommended configuration of a Pokemon Variety and specifies which
    /// nature, ability, moves, held items and EV distributions to choose for this Pokemon Variety.
    /// </summary>
    [Table("Build")]
    [Sheet("builds")]
    public class Build : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<Build>().HasIndexedHashes();

            builder.Entity<Build>()
                .HasOne(b => b.PokemonVariety)
                .WithMany(pv => pv.Builds)
                .HasForeignKey(b => b.PokemonVarietyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Build>()
                .HasOne(b => b.Ability)
                .WithMany()
                .HasForeignKey(b => b.AbilityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Build>()
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

        public string Name { get; set; }

        public string Description { get; set; }

        public int AttackEv { get; set; }
        public int DefenseEv { get; set; }
        public int SpecialAttackEv { get; set; }
        public int SpecialDefenseEv { get; set; }
        public int SpeedEv { get; set; }
        public int HitPointsEv { get; set; }

        [ForeignKey("PokemonVarietyId")]
        public PokemonVariety PokemonVariety { get; set; }

        public int PokemonVarietyId { get; set; }

        [NotMapped]
        public string PokemonVarietyName { internal get; set; }

        [ForeignKey("AbilityId")]
        public Ability Ability { get; set; }

        public int AbilityId { get; set; }

        [NotMapped]
        public string AbilityName { internal get; set; }

        public List<NatureOption> NatureOptions { get; set; } = new();

        public List<ItemOption> ItemOptions { get; set; } = new();

        public List<MoveOption> MoveOptions { get; set; } = new();

        public override string ToString()
        {
            return $"{PokemonVariety} - {Name}";
        }
    }
}