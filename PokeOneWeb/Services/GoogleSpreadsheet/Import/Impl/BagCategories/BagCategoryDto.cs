namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.BagCategories
{
    public class BagCategoryDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }
        public int SortIndex { get; set; }
    }
}
