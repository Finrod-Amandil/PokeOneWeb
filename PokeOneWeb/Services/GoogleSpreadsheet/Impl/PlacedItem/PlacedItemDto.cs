namespace PokeOneWeb.Services.GoogleSpreadsheet.Impl.PlacedItem
{
    public class PlacedItemDto : ISpreadsheetDto
    {
        public string LocationName { get; set; }

        public int Quantity { get; set; }

        public string ItemName { get; set; }

        public string PlacementDescription { get; set; }

        public bool IsHidden { get; set; }

        public bool IsConfirmed { get; set; }

        public string Requirements { get; set; }

        public string Notes { get; set; }
    }
}
