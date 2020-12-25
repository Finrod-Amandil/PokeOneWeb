using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("ItemOption")]
    public class ItemOption
    {
        public int Id { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }
        public int ItemId { get; set; }

        [ForeignKey("BuildId")]
        public Build Build { get; set; }
        public int BuildId { get; set; }
    }
}
