namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Abilities
{
    public class AbilitySheetDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }
        public string PokeApiName { get; set; }
        public string ShortEffect { get; set; }
        public string Effect { get; set; }
    }
}
