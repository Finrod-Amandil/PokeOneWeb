using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("HuntingConfiguration")]
    public class HuntingConfiguration
    {
        public int Id { get; set; }

        [ForeignKey("PokemonVarietyId")]
        public PokemonVariety PokemonVariety { get; set; }
        public int PokemonVarietyId { get; set; }

        [ForeignKey("NatureId")]
        public Nature Nature { get; set; }
        public int NatureId { get; set; }

        [ForeignKey("AbilityId")]
        public Ability Ability { get; set; }
        public int AbilityId { get; set; }
    }
}
