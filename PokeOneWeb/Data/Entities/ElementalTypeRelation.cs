using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("ElementalTypeRelation")]
    public class ElementalTypeRelation
    {
        public int Id { get; set; }

        [ForeignKey("AttackingTypeId")]
        public ElementalType AttackingType { get; set; }
        public int AttackingTypeId { get; set; }

        [ForeignKey("DefendingTypeId")]
        public ElementalType DefendingType { get; set; }
        public int DefendingTypeId { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal AttackEffectivity { get; set; }
    }
}
