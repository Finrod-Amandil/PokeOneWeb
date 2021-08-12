namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.HuntingConfigurations
{
    public class HuntingConfigurationDto : ISpreadsheetEntityDto
    {
        public string PokemonVarietyName { get; set; }

        public string Nature { get; set; }

        public string Ability { get; set; }
    }
}
