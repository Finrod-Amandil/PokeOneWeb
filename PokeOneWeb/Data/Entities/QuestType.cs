using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("QuestType")]
    public class QuestType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Quest> Quests { get; set; }
    }
}
