namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Abilities
{
    public class AbilityDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }
        public string PokeApiName { get; set; }
        public string ShortEffect { get; set; }
        public string Effect { get; set; }
    }
}
