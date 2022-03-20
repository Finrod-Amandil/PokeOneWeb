using PokeOneWeb.Data.Entities;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Spawns
{
    public class SpawnSheetRowParser : SheetRowParser<SpawnSheetDto>
    {
        protected override int RequiredValueCount => 5;

        protected override List<Action<SpawnSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.LocationName = ParseAsNonEmptyString(value),
            (dto, value) => dto.PokemonForm = ParseAsNonEmptyString(value),
            (dto, value) => dto.Season = ParseAsNonEmptyString(value),
            (dto, value) => dto.TimeOfDay = ParseAsNonEmptyString(value),
            (dto, value) => dto.SpawnType = ParseAsNonEmptyString(value),
            (dto, value) => dto.SpawnCommonality = ParseAsString(value),
            (dto, value) => dto.SpawnProbability = ParseAsString(value),
            (dto, value) => dto.EncounterCount = ParseAsInt(value, 0),
            (dto, value) => dto.IsConfirmed = ParseAsBoolean(value, true),
            (dto, value) => dto.LowestLvl = ParseAsInt(value, 0),
            (dto, value) => dto.HighestLvl = ParseAsInt(value, 0),
            (dto, value) => dto.Notes = ParseAsString(value),
        };
    }
}
