using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("ElementalType")]
    public class ElementalType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<ElementalTypeRelation> AttackingDamageRelations { get; set; }
        public List<ElementalTypeRelation> DefendingDamageRelations { get; set; }
        public List<ElementalTypeCombination> ElementalTypeCombinationsAsPrimaryType { get; set; }
        public List<ElementalTypeCombination> ElementalTypeCombinationsAsSecondaryType { get; set; }
        public List<Move> Moves { get; set; }
    }
}
