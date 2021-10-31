using System.Collections.Generic;

namespace PokeOneWeb.Dtos
{
    public class ItemDto
    {
        public string ResourceName { get; set; }

        public int SortIndex { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Effect { get; set; }

        public bool IsAvailable { get; set; }

        public string SpriteName { get; set; }

        public string BagCategoryName { get; set; }

        public int BagCategorySortIndex { get; set; }

        public IEnumerable<PlacedItemDto> PlacedItems { get; set; }
    }
}
