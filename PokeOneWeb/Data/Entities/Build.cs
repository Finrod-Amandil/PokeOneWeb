using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("Build")]
    public class Build
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey("PokemonVarietyId")]
        public PokemonVariety PokemonVariety { get; set; }
        public int PokemonVarietyId { get; set; }

        public List<MoveOption> Moves { get; set; }

        public List<ItemOption> Item { get; set; }

        public List<NatureOption> Nature { get; set; }

        [ForeignKey("AbilityId")]
        public Ability Ability { get; set; }
        public int AbilityId { get; set; }

        [ForeignKey("EvDistributionId")]
        public Stats EvDistribution { get; set; }
        public int? EvDistributionId { get; set; }
    }
}
