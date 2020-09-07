using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("EvolutionChain")]
    public class EvolutionChain
    {
        public int Id { get; set; }

        public int PokeApiId { get; set; }

        public List<Evolution> Evolutions { get; set; }
    }
}
