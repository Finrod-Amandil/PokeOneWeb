using System;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.LearnableMoveLearnMethods
{
    public class LearnableMoveLearnMethodXSheetRowParser : XSheetRowParser<LearnableMoveLearnMethodSheetDto>
    {
        protected override int RequiredValueCount => 5;

        protected override List<Action<LearnableMoveLearnMethodSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.PokemonSpeciesPokedexNumber = ParseAsInt(value),
            (dto, value) => dto.PokemonVarietyName = ParseAsNonEmptyString(value),
            (dto, value) => dto.MoveName = ParseAsNonEmptyString(value),
            (dto, value) => dto.LearnMethod = ParseAsNonEmptyString(value),
            (dto, value) => dto.IsAvailable = ParseAsBoolean(value),
            (dto, value) => dto.Generation = ParseAsString(value),
            (dto, value) => dto.LevelLearnedAt = ParseAsInt(value, 0),
            (dto, value) => dto.RequiredItemName = ParseAsString(value),
            (dto, value) => dto.TutorName = ParseAsString(value),
            (dto, value) => dto.TutorLocation = ParseAsString(value),
            (dto, value) => dto.Comments = ParseAsString(value),
        };
    }
}