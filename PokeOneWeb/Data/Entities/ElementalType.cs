using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("ElementalType")]
    public class ElementalType
    {
        public int Id { get; set; }

        public string PokeApiName { get; set; }
        
        [Required]
        public string Name { get; set; }

        public List<ElementalTypeRelation> AttackingDamageRelations { get; set; } = new List<ElementalTypeRelation>();
        public List<ElementalTypeRelation> DefendingDamageRelations { get; set; } = new List<ElementalTypeRelation>();
        public List<ElementalTypeCombination> ElementalTypeCombinationsAsPrimaryType { get; set; } = new List<ElementalTypeCombination>();
        public List<ElementalTypeCombination> ElementalTypeCombinationsAsSecondaryType { get; set; } = new List<ElementalTypeCombination>();
        public List<Move> Moves { get; set; }
    }
}
