namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Evolutions
{
    public class EvolutionSheetDto : ISpreadsheetEntityDto
    {
        public int BasePokemonSpeciesPokedexNumber { get; set; }

        public string BasePokemonSpeciesName { get; set; }

        public string BasePokemonVarietyName { get; set; }

        public int BaseStage { get; set; }

        public string EvolvedPokemonVarietyName { get; set; }

        public int EvolvedStage { get; set; }

        public string EvolutionTrigger { get; set; }

        public bool IsReversible { get; set; }

        public bool IsAvailable { get; set; }

        public bool DoInclude { get; set; }
    }
}
