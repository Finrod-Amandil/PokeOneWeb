using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Data.Entities
{
    [Table("Build")]
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
        [Key] public int Id { get; set; }

        //INDEXED
        [Required] public string Hash { get; set; }

        //INDEXED
        [Required] public string IdHash { get; set; }

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

        [ForeignKey("PokemonVarietyId")] public PokemonVariety PokemonVariety { get; set; }
        public int PokemonVarietyId { get; set; }

        [ForeignKey("AbilityId")] public Ability Ability { get; set; }
        public int AbilityId { get; set; }

        public List<NatureOption> NatureOptions { get; set; } = new List<NatureOption>();

        public List<ItemOption> ItemOptions { get; set; } = new List<ItemOption>();

        public List<MoveOption> MoveOptions { get; set; } = new List<MoveOption>();


        public override string ToString()
        {
            return $"{PokemonVariety} - {Name}";
        }
    }
}
