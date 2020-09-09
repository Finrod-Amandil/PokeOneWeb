using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("Location")]
    public class Location
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int SortIndex { get; set; }

        [ForeignKey("LocationGroupId")]
        public LocationGroup LocationGroup { get; set; }
        public int LocationGroupId { get; set; }

        public bool IsDiscoverable { get; set; }

        public string Notes { get; set; }

        public List<Spawn> PokemonSpawns { get; set; }
    }
}
