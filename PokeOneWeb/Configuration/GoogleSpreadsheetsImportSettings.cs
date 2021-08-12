namespace PokeOneWeb.Configuration
{
    public class GoogleSpreadsheetsImportSettings
    {
        public int MinTimeBetweenSheets { get; set; }

        public string SheetsListSpreadsheetId { get; set; }

        public string SheetsListSheetName { get; set; }

        public GoogleSpreadsheetsSheetNames SheetNames { get; set; }

        public GoogleSpreadsheetsSheetPrefixes SheetPrefixes { get; set; }
    }
}
