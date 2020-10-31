using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("ElementalTypeCombination")]
    public class ElementalTypeCombination
    {
        public int Id { get; set; }
        [ForeignKey("PrimaryTypeId")]
        public ElementalType PrimaryType { get; set; }
        public int PrimaryTypeId { get; set; }

        [ForeignKey("SecondaryTypeId")]
        public ElementalType SecondaryType { get; set; }
        public int? SecondaryTypeId { get; set; }

        public List<PokemonVariety> PokemonVarieties { get; set; } = new List<PokemonVariety>();
    }
}
