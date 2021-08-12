namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.ElementalTypes
{
    public class ElementalTypeDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }

        public string PokeApiName { get; set; }
    }
}
