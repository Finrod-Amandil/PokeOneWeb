using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("LearnableMoveLearnMethod")]
    public class LearnableMoveLearnMethod
    {
        public int Id { get; set; }

        [ForeignKey("LearnableMoveId")]
        public LearnableMove LearnableMove { get; set; }
        public int LearnableMoveId { get; set; }

        [ForeignKey("MoveLearnMethodId")]
        public MoveLearnMethod MoveLearnMethod { get; set; }
        public int MoveLearnMethodId { get; set; }

        /// <summary>
        /// What item (TM / HM) is required to teach this move. May not be used.
        /// </summary>
        public Item RequiredItem { get; set; }

        public int? LevelLearnedAt { get; set; }

        public bool IsAvailable { get; set; }

        /// <summary>
        /// The Price to teach this move from a tutor. May require multiple currencies at once.
        /// </summary>
        public List<LearnableMoveLearnMethodPrice> Price { get; set; } = new List<LearnableMoveLearnMethodPrice>();
    }
}
