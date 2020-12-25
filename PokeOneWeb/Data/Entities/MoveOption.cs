using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("MoveOption")]
    public class MoveOption
    {
        public int Id { get; set; }

        [ForeignKey("MoveId")]
        public Move Move { get; set; }
        public int MoveId { get; set; }

        [ForeignKey("BuildId")]
        public Build Build { get; set; }
        public int BuildId { get; set; }

        public int Slot { get; set; }
    }
}
