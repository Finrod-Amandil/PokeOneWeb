using System;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveLearnMethodLocations
{
    public class MoveLearnMethodLocationXSheetRowParser : XSheetRowParser<MoveLearnMethodLocationSheetDto>
    {
        protected override int RequiredValueCount => 4;

        protected override List<Action<MoveLearnMethodLocationSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.MoveLearnMethodName = ParseAsNonEmptyString(value),
            (dto, value) => dto.TutorType = ParseAsNonEmptyString(value),
            (dto, value) => dto.NpcName = ParseAsNonEmptyString(value),
            (dto, value) => dto.LocationName = ParseAsNonEmptyString(value),
            (dto, value) => dto.PlacementDescription = ParseAsString(value),
            (dto, value) => dto.PokeDollarPrice = ParseAsInt(value, 0),
            (dto, value) => dto.PokeGoldPrice = ParseAsInt(value, 0),
            (dto, value) => dto.BigMushroomPrice = ParseAsInt(value, 0),
            (dto, value) => dto.HeartScalePrice = ParseAsInt(value, 0),
        };
    }
}