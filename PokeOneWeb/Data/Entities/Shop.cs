using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("Shop")]
    public class Shop
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public int? LocationId { get; set; }

        public List<ShopSoldItem> SoldItems { get; set; } = new List<ShopSoldItem>();
        public List<ShopBoughtItem> BoughtItems { get; set; } = new List<ShopBoughtItem>();
    }
}
