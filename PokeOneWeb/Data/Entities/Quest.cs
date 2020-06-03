using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("Quest")]
    public class Quest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string From { get; set; }

        public int? ExperienceReward { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? MoneyReward { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public int? LocationId { get; set; }

        [ForeignKey("QuestTypeId")]
        public QuestType QuestType { get; set; }
        public int QuestTypeId { get; set; }

        public List<QuestItemReward> ItemRewards { get; set; }
    }
}
