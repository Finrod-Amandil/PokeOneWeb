using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("Item")]
    public class Item
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey("BagCategoryId")]
        public BagCategory BagCategory { get; set; }
        public int BagCategoryId { get; set; }

        public List<ShopSoldItem> SoldItems { get; set; }
        public List<ShopBoughtItem> BoughtItems { get; set; }
        public List<PlacedItem> PlacedItems { get; set; }
        public List<QuestItemReward> QuestRewardItems { get; set; }
        public List<PokemonHeldItem> HeldItems { get; set; }
    }
}
