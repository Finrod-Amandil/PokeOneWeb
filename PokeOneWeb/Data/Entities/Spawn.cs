using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("Spawn")]
    public class Spawn
    {
        public int Id { get; set; }

        [ForeignKey("PokemonFormId")]
        public PokemonForm PokemonForm { get; set; }
        public int PokemonFormId { get; set; }

        public bool IsConfirmed { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public int LocationId { get; set; }

        [ForeignKey("SpawnTypeId")]
        public SpawnType SpawnType { get; set; }
        public int SpawnTypeId { get; set; }

        public string Notes { get; set; }

        public List<SpawnOpportunity> SpawnOpportunities { get; set; } = new List<SpawnOpportunity>();
    }
}
