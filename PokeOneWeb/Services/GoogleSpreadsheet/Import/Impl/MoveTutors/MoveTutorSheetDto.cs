namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MoveTutors
{
    public class MoveTutorSheetDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }
        public string LocationName { get; set; }
        public string PlacementDescription { get; set; }
    }
}
