namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.BagCategories
{
    public class BagCategorySheetDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }
        public int SortIndex { get; set; }
    }
}
