namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.BagCategories
{
    public class BagCategorySheetDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }
        public int SortIndex { get; set; }
    }
}