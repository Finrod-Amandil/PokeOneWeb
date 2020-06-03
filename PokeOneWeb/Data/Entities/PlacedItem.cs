using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("PlacedItem")]
    public class PlacedItem
    {
        public int Id { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }
        public int ItemId { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public int LocationId { get; set; }

        public string PlacementDescription { get; set; }

        public string Notes { get; set; }

        public bool IsHidden { get; set; }
    }
}
