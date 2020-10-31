using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("PokemonForm")]
    public class PokemonForm
    {
        public int Id { get; set; }

        public string PokeApiName { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("PokemonVarietyId")]
        public PokemonVariety PokemonVariety { get; set; }
        public int PokemonVarietyId { get; set; }

        [ForeignKey("AvailabilityId")]
        public PokemonAvailability Availability { get; set; }
        public int? AvailabilityId { get; set; }

        public List<Spawn> PokemonSpawns { get; set; } = new List<Spawn>();
    }
}
