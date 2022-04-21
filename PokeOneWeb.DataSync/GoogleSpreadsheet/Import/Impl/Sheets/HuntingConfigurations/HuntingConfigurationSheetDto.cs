namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.HuntingConfigurations
{
    public class HuntingConfigurationSheetDto : XISpreadsheetEntityDto
    {
        public string PokemonVarietyName { get; set; }

        public string Nature { get; set; }

        public string Ability { get; set; }
    }
}