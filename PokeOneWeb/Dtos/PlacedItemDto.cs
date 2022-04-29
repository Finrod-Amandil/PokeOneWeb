namespace PokeOneWeb.WebApi.Dtos
{
    public class PlacedItemDto
    {
        public string ItemResourceName { get; set; }

        public string ItemName { get; set; }

        public string ItemSpriteName { get; set; }

        public string RegionName { get; set; }

        public string RegionColor { get; set; }

        public string LocationName { get; set; }

        public string LocationResourceName { get; set; }

        public int LocationSortIndex { get; set; }

        public int SortIndex { get; set; }

        public int Index { get; set; }

        public string PlacementDescription { get; set; }

        public bool IsHidden { get; set; }

        public bool IsConfirmed { get; set; }

        public int Quantity { get; set; }

        public string Notes { get; set; }

        public string Screenshot { get; set; }
    }
}