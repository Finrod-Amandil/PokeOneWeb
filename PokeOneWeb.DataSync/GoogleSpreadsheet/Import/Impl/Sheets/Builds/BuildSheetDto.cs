namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Builds
{
    public class BuildSheetDto : ISpreadsheetEntityDto
    {
        public string PokemonVarietyName { get; set; }

        public string BuildName { get; set; }

        public string BuildDescription { get; set; }

        public string Move1 { get; set; }

        public string Move2 { get; set; }

        public string Move3 { get; set; }

        public string Move4 { get; set; }

        public string Item { get; set; }

        public string Nature { get; set; }

        public string Ability { get; set; }

        public string EvDistribution { get; set; }
    }
}
