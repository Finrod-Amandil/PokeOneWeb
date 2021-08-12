namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MoveTutors
{
    public class MoveTutorDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }
        public string LocationName { get; set; }
        public string PlacementDescription { get; set; }
    }
}
