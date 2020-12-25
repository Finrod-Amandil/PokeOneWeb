namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.ElementalTypes
{
    public class ElementalTypeDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }

        public string PokeApiName { get; set; }
    }
}
