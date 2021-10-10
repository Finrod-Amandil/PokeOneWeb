using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("PokemonVariety")]
    public class PokemonVariety
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<PokemonVariety>().HasIndex(pv => pv.Name).IsUnique();
            builder.Entity<PokemonVariety>().HasIndex(pv => pv.ResourceName).IsUnique();

            builder.Entity<PokemonVariety>()
                .HasOne(v => v.PokemonSpecies)
                .WithMany(s => s.Varieties)
                .HasForeignKey(v => v.PokemonSpeciesId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<PokemonVariety>()
                .HasOne(p => p.DefaultForm)
                .WithMany()
                .HasForeignKey(p => p.DefaultFormId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<PokemonVariety>()
                .HasOne(p => p.PrimaryType)
                .WithMany()
                .HasForeignKey(p => p.PrimaryTypeId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<PokemonVariety>()
                .HasOne(p => p.SecondaryType)
                .WithMany()
                .HasForeignKey(p => p.SecondaryTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<PokemonVariety>()
                .HasOne(p => p.PrimaryAbility)
                .WithMany(a => a.PokemonVarietiesAsPrimaryAbility)
                .HasForeignKey(p => p.PrimaryAbilityId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<PokemonVariety>()
                .HasOne(p => p.SecondaryAbility)
                .WithMany(a => a.PokemonVarietiesAsSecondaryAbility)
                .HasForeignKey(p => p.SecondaryAbilityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<PokemonVariety>()
                .HasOne(p => p.HiddenAbility)
                .WithMany(a => a.PokemonVarietiesAsHiddenAbility)
                .HasForeignKey(p => p.HiddenAbilityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<PokemonVariety>()
                .HasOne(p => p.PvpTier)
                .WithMany()
                .HasForeignKey(p => p.PvpTierId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string ResourceName { get; set; }

        [Required]
        public string Name { get; set; }

        public string PokeApiName { get; set; }
        public bool DoInclude { get; set; }
        public bool IsMega { get; set; }
        public bool IsFullyEvolved { get; set; }
        public int Generation { get; set; }
        public int CatchRate { get; set; }
        public bool HasGender { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal MaleRatio { get; set; }
        public int EggCycles { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal Height { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal Weight { get; set; }
        public int ExpYield { get; set; }

        //BASE STATS
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public int HitPoints { get; set; }

        //EV Yields
        public int AttackEv { get; set; }
        public int DefenseEv { get; set; }
        public int SpecialAttackEv { get; set; }
        public int SpecialDefenseEv { get; set; }
        public int SpeedEv { get; set; }
        public int HitPointsEv { get; set; }

        public string Notes { get; set; }

        [ForeignKey("PokemonSpeciesId")]
        public PokemonSpecies PokemonSpecies { get; set; }
        public int PokemonSpeciesId { get; set; }

        [ForeignKey("DefaultFormId")]
        public PokemonForm DefaultForm { get; set; }
        public int? DefaultFormId { get; set; }

        [ForeignKey("PrimaryTypeId")]
        public ElementalType PrimaryType { get; set; }
        public int PrimaryTypeId { get; set; }

        [ForeignKey("SecondaryTypeId")]
        public ElementalType SecondaryType { get; set; }
        public int? SecondaryTypeId { get; set; }

        [ForeignKey("PrimaryAbilityId")]
        public Ability PrimaryAbility { get; set; }
        public int PrimaryAbilityId { get; set; }

        [ForeignKey("SecondaryAbilityId")]
        public Ability SecondaryAbility { get; set; }
        public int? SecondaryAbilityId { get; set; }

        [ForeignKey("HiddenAbilityId")]
        public Ability HiddenAbility { get; set; }
        public int? HiddenAbilityId { get; set; }

        [ForeignKey("PvpTierId")]
        public PvpTier PvpTier { get; set; }
        public int PvpTierId { get; set; }


        public List<PokemonForm> Forms { get; set; }
        public List<LearnableMove> LearnableMoves { get; set; } = new List<LearnableMove>();
        public List<Build> Builds { get; set; }
        public List<HuntingConfiguration> HuntingConfigurations { get; set; }
        public List<PokemonVarietyUrl> Urls { get; set; } = new List<PokemonVarietyUrl>();


        public override string ToString()
        {
            return $"{Name} ({ResourceName})";
        }
    }
}
