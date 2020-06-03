﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("PokemonSpecies")]
    public class PokemonSpecies
    {
        public int Id { get; set; }

        public int PokedexNumber { get; set; }

        [Required]
        public string Name { get; set; }

        public List<PokemonVariety> Varieties { get; set; }

        [ForeignKey("DefaultVarietyId")]
        public PokemonVariety DefaultVariety { get; set; }
        public int DefaultVarietyId { get; set; }
    }
}
