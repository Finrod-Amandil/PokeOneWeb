﻿namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.LearnableMoveLearnMethods
{
    public class LearnableMoveLearnMethodSheetDto : ISpreadsheetEntityDto
    {
        public string PokemonVarietyName { get; set; }

        public string MoveName { get; set; }

       public string LearnMethod { get; set; }

        public bool IsAvailable { get; set; }

        public int? LevelLearnedAt { get; set; }

        public string RequiredItemName { get; set; }

        public string TutorName { get; set; }

        public string TutorLocation { get; set; }

        public string Comments { get; set; }
    }
}