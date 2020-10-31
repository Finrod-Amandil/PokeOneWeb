using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Classified method of how a move can be learnt, i.e.
    /// Level-Up
    /// Item (TM/HM)
    /// Egg-move-tutor (one entry for all egg move tutors, as all of them teach the same moves)
    /// Move reminder
    /// Green shard tutor
    /// </summary>
    [Table("MoveLearnMethod")]
    public class MoveLearnMethod
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<MoveLearnMethodLocation> Locations { get; set; } = new List<MoveLearnMethodLocation>();
    }
}
