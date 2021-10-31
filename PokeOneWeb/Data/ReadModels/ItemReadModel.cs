using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("ItemReadModel")]
    public class ItemReadModel : IReadModel
    {
        public int Id { get; set; }

        [Required]
        public int ApplicationDbId { get; set; }

        public string ResourceName { get; set; }

        public int SortIndex { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Effect { get; set; }

        public bool IsAvailable { get; set; }

        public string SpriteName { get; set; }

        public string BagCategoryName { get; set; }

        public int BagCategorySortIndex { get; set; }

        public List<PlacedItemReadModel> PlacedItems { get; set; }
    }
}
