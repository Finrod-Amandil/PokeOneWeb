using System.Collections.Generic;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    public class ItemReadListModel : IReadModel
    {
        public string Name { get; set; }
        public string ResourceName { get; set; }
        public string SpriteName { get; set; }

        public int SortIndex { get; set; }
        public string Description { get; set; }
        public string Effect { get; set; }
        public bool IsAvailable { get; set; }
        public string BagCategoryName { get; set; }
        public int BagCategorySortIndex { get; set; }
    }

    public class ItemReadModel : ItemReadListModel
    {
        public List<PlacedItemReadModel> PlacedItems { get; set; } = new();
    }
}