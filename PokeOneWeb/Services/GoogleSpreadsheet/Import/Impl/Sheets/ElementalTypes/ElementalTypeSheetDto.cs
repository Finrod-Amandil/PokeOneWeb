namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.ElementalTypes
{
    public class ElementalTypeSheetDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }

        public string PokeApiName { get; set; }
    }
}
