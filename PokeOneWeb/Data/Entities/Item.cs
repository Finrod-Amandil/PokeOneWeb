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
        public string ResourceName { get; set; }

        [Required]
        public string Name { get; set; }

        public string PokeApiName { get; set; }

        public int? PokeoneItemId { get; set; }

        public string Description { get; set; }

        public string Effect { get; set; }

        public bool IsAvailable { get; set; }

        public bool DoInclude { get; set; }

        public int SortIndex { get; set; }

        [ForeignKey("BagCategoryId")]
        public BagCategory BagCategory { get; set; }
        public int BagCategoryId { get; set; }

        public List<ShopSoldItem> SoldItems { get; set; } = new List<ShopSoldItem>();
        public List<ShopBoughtItem> BoughtItems { get; set; } = new List<ShopBoughtItem>();
        public List<PlacedItem> PlacedItems { get; set; } = new List<PlacedItem>();
        public List<QuestItemReward> QuestRewardItems { get; set; } = new List<QuestItemReward>();
        public List<PokemonHeldItem> HeldItems { get; set; } = new List<PokemonHeldItem>();
    }
}
