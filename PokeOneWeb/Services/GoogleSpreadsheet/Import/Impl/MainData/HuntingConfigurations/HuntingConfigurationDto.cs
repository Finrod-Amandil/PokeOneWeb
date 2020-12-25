namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.HuntingConfigurations
{
    public class HuntingConfigurationDto : ISpreadsheetEntityDto
    {
        public string PokemonVarietyName { get; set; }

        public string Nature { get; set; }

        public string Ability { get; set; }
    }
}
