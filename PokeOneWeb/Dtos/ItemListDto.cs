namespace PokeOneWeb.WebApi.Dtos
{
    public class ItemListDto
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
    }
}
