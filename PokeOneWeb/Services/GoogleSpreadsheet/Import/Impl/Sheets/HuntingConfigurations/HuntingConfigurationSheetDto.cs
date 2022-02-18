namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.HuntingConfigurations
{
    public class HuntingConfigurationSheetDto : ISpreadsheetEntityDto
    {
        public string PokemonVarietyName { get; set; }

        public string Nature { get; set; }

        public string Ability { get; set; }
    }
}
