namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.Spawns
{
    public class SpawnDto : ISpreadsheetEntityDto
    {
        public string LocationName { get; set; }

        public string PokemonForm { get; set; }

        public string Season { get; set; }

        public string TimeOfDay { get; set; }

        public string SpawnType { get; set; }

        public string SpawnCommonality { get; set; }

        public string SpawnProbability { get; set; }

        public int? EncounterCount { get; set; }

        public bool IsConfirmed { get; set; }

        public string Notes { get; set; }
    }
}
