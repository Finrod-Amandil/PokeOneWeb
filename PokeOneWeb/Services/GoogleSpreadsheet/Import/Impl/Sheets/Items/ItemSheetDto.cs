namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Items
{
    public class ItemSheetDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public bool DoInclude { get; set; }
        public string ResourceName { get; set; }
        public int SortIndex { get; set; }
        public string BagCategoryName { get; set; }
        public string PokeApiName { get; set; }
        public int PokeOneItemId { get; set; }
        public string Description { get; set; }
        public string Effect { get; set; }
        public string SpriteName { get; set; }
    }
}
