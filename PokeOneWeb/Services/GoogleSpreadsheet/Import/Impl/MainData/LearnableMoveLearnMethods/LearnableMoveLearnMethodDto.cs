using System.Collections.Generic;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.TutorMoves;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.LearnableMoveLearnMethods
{
    public class LearnableMoveLearnMethodDto : ISpreadsheetEntityDto
    {
        public string PokemonVarietyName { get; set; }

        public string MoveName { get; set; }

       public LearnMethod LearnMethod { get; set; }

        public bool IsAvailable { get; set; }

        public int? LevelLearnedAt { get; set; }

        public string RequiredItemName { get; set; }

        public string TutorName { get; set; }

        public string TutorLocation { get; set; }

        public string Comments { get; set; }

        public List<TutorMoveDto> TutorMoveDtos { get; set; } = new List<TutorMoveDto>();
    }
}
