using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Information, on a specific Move that can be learnt by a specific Pokémon.
    /// </summary>
    [Table("LearnableMove")]
    public class LearnableMove
    {
        public int Id { get; set; }

        [ForeignKey("MoveId")]
        public Move Move { get; set; }
        public int MoveId { get; set; }

        [ForeignKey("PokemonVarietyId")]
        public PokemonVariety PokemonVariety { get; set; }
        public int ProkemonVarietyId { get; set; }

        public bool IsAvailable { get; set; }

        public List<LearnableMoveLearnMethod> LearnMethods { get; set; }
    }
}
