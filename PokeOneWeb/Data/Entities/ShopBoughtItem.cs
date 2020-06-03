using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("ShopBoughtItem")]
    public class ShopBoughtItem
    {
        public int Id { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }
        public int ItemId { get; set; }

        [ForeignKey("ShopId")]
        public Shop Shop { get; set; }
        public int ShopId { get; set; }

        [ForeignKey("PriceId")]
        public CurrencyAmount Price { get; set; }
        public int PriceId { get; set; }

        public string Notes { get; set; }
    }
}
