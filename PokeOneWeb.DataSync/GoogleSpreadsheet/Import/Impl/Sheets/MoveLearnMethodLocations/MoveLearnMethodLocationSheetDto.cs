namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveLearnMethodLocations
{
    public class MoveLearnMethodLocationSheetDto : ISpreadsheetEntityDto
    {
        public string MoveLearnMethodName { get; set; }
        public string TutorType { get; set; }
        public string NpcName { get; set; }
        public string LocationName { get; set; }
        public string PlacementDescription { get; set; }
        public int PokeDollarPrice { get; set; }
        public int PokeGoldPrice { get; set; }
        public int BigMushroomPrice { get; set; }
        public int HeartScalePrice { get; set; }
    }
}