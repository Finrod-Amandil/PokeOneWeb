namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.PlacedItems
{
    public class PlacedItemSheetDto : ISpreadsheetEntityDto
    {
        public string LocationName { get; set; }

        public int Quantity { get; set; }

        public string ItemName { get; set; }

        public int SortIndex { get; set; }

        public int Index { get; set; }

        public string PlacementDescription { get; set; }

        public bool IsHidden { get; set; }

        public bool IsConfirmed { get; set; }

        public string Requirements { get; set; }

        public string ScreenshotName { get; set; }

        public string Notes { get; set; }
    }
}
