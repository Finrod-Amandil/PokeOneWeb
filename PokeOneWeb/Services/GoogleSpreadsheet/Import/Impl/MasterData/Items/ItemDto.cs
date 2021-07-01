namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Items
{
    public class ItemDto : ISpreadsheetEntityDto
    {
        public string ItemName { get; set; }
        public string ResourceName { get; set; }
        public string PokeApiName { get; set; }
        public bool IsAvailable { get; set; }
        public bool DoInclude { get; set; }
        public int PokeOneItemId { get; set; }
        public int SortIndex { get; set; }
        public string BagCategoryName { get; set; }
        public int BagCategorySortIndex { get; set; }
        public string Description { get; set; }
        public string Effect { get; set; }
        public string SpriteName { get; set; }
    }
}
