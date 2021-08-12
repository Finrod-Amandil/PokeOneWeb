using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Data.Entities
{
    [Table("ElementalTypeRelation")]
    public class ElementalTypeRelation : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<ElementalTypeRelation>().HasIndexedHashes();

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

            builder.Entity<ElementalTypeRelation>()
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

        [Column(TypeName = "decimal(4,1)")]
        public decimal AttackEffectivity { get; set; }

        [ForeignKey("AttackingTypeId")]
        public ElementalType AttackingType { get; set; }
        public int AttackingTypeId { get; set; }

        [ForeignKey("DefendingTypeId")]
        public ElementalType DefendingType { get; set; }
        public int DefendingTypeId { get; set; }

        
        public override string ToString()
        {
            return $"{AttackingType} *> {DefendingType}: x{AttackEffectivity}";
        }
    }
}
