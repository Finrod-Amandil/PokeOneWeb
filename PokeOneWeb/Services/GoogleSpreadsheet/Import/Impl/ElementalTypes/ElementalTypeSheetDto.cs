namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.ElementalTypes
{
    public class ElementalTypeSheetDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }

        public string PokeApiName { get; set; }
    }
}
