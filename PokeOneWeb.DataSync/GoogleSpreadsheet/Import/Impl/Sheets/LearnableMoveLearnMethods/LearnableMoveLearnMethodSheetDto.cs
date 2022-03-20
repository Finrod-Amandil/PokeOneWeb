namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.LearnableMoveLearnMethods
{
    public class LearnableMoveLearnMethodSheetDto : ISpreadsheetEntityDto
    {
        // Only used for sorting in spreadsheets, no import required
        public int PokemonSpeciesPokedexNumber { get; set; }

        public string PokemonVarietyName { get; set; }

        public string MoveName { get; set; }

       public string LearnMethod { get; set; }

        public bool IsAvailable { get; set; }

        // Only used as editing aid in spreadsheets, no import required
        public string Generation { get; set; }

        public int? LevelLearnedAt { get; set; }

        public string RequiredItemName { get; set; }

        public string TutorName { get; set; }

        public string TutorLocation { get; set; }

        public string Comments { get; set; }
    }
}
