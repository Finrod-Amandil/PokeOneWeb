using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Attributes;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Defines, how effective a move with a specific Attacking Type is when used against a
    /// (single typed) Pokemon with a specific Defending Type. The effectivity can be x1 (normal),
    /// x2 (super effective), x0.5 (not very effective) or x0 (immune).
    /// </summary>
    [Table("ElementalTypeRelation")]
    [Sheet("elemental_type_relations")]
    public class ElementalTypeRelation : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<ElementalTypeRelation>().HasIndexedHashes();

            builder.Entity<ElementalTypeRelation>()
                .HasOne(x => x.ImportSheet)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<ElementalTypeRelation>()
                .HasOne(etr => etr.AttackingType)
                .WithMany(et => et.AttackingDamageRelations)
                .HasForeignKey(etr => etr.AttackingTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ElementalTypeRelation>()
                .HasOne(etr => etr.DefendingType)
                .WithMany(et => et.DefendingDamageRelations)
                .HasForeignKey(etr => etr.DefendingTypeId)
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

        [Column(TypeName = "decimal(4,1)")]
        public decimal AttackEffectivity { get; set; }

        [ForeignKey("AttackingTypeId")]
        public ElementalType AttackingType { get; set; }

        public int AttackingTypeId { get; set; }

        [NotMapped]
        public string AttackingTypeName { internal get; set; }

        [ForeignKey("DefendingTypeId")]
        public ElementalType DefendingType { get; set; }

        public int DefendingTypeId { get; set; }

        [NotMapped]
        public string DefendingTypeName { internal get; set; }

        public override string ToString()
        {
            return $"{AttackingType?.ToString() ?? AttackingTypeName} -> " +
                   $"{DefendingType?.ToString() ?? DefendingTypeName}: x{AttackEffectivity}";
        }
    }
}