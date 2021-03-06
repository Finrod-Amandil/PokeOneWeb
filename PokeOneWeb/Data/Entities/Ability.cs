﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// An Ability is a passive effect every Pokémon has. Every Pokémon has exactly one Ability, but a PokémonSpecies may have multiple
    /// Abilities available, out of which the Ability of a Pokémon is chosen. A PokémonSpecies may have up to 3 Abilities.
    /// These Abilities are labeled Primary, Secondary and Hidden Ability. The latter two may not exist on certain species.
    /// </summary>
    [Table("Ability")]
    public class Ability
    {
        public int Id { get; set; }

        public string PokeApiName { get; set; }

        [Required]
        public string Name { get; set; }

        public string EffectDescription { get; set; }

        public string EffectShortDescription { get; set; }

        public List<PokemonVariety> PokemonVarietiesAsPrimaryAbility { get; set; } = new List<PokemonVariety>();
        public List<PokemonVariety> PokemonVarietiesAsSecondaryAbility { get; set; } = new List<PokemonVariety>();
        public List<PokemonVariety> PokemonVarietiesAsHiddenAbility { get; set; } = new List<PokemonVariety>();
    }
}
