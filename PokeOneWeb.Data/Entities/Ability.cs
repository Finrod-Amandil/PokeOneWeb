using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// An Ability is a passive effect every Pokemon has. Every Pokemon has exactly one Ability, but a PokemonSpecies may have multiple
    /// Abilities available, out of which the Ability of a Pokemon is chosen. A PokemonSpecies may have up to three Abilities.
    /// These Abilities are labeled Primary, Secondary and Hidden Ability. The latter two may not exist on certain species.
    /// </summary>
    [Table("Ability")]
    public class Ability : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<Ability>().HasIndexedHashes();
            builder.Entity<Ability>().HasIndex(a => a.Name).IsUnique();

            builder.Entity<Ability>()
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

        public string EffectDescription { get; set; }

        public string EffectShortDescription { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal AttackBoost { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal SpecialAttackBoost { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal DefenseBoost { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal SpecialDefenseBoost { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal SpeedBoost { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal HitPointsBoost { get; set; }

        public string BoostConditions { get; set; }

        public List<PokemonVariety> PokemonVarietiesAsPrimaryAbility { get; set; } = new();
        public List<PokemonVariety> PokemonVarietiesAsSecondaryAbility { get; set; } = new();
        public List<PokemonVariety> PokemonVarietiesAsHiddenAbility { get; set; } = new();

        public override string ToString()
        {
            return Name;
        }
    }
}