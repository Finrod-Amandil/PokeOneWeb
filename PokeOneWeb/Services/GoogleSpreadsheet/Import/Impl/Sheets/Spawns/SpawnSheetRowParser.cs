using System.Collections.Generic;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Spawns
{
    public class SpawnSheetRowParser : ISheetRowParser<SpawnSheetDto>
    {
        public SpawnSheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 5)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new SpawnSheetDto
            {
                LocationName = values[0] as string,
                PokemonForm = values[1] as string,
                Season = values[2] as string ?? Season.ANY,
                TimeOfDay = values[3] as string ?? TimeOfDay.ANY,
                SpawnType = values[4] as string
            };

            if (value.LocationName is null)
            {
                throw new InvalidRowDataException($"Tried to read Spawn, but required field {nameof(value.LocationName)} was empty.");
            }

            if (value.PokemonForm is null)
            {
                throw new InvalidRowDataException($"Tried to read Spawn, but required field {nameof(value.PokemonForm)} was empty.");
            }

            if (value.SpawnType is null)
            {
                throw new InvalidRowDataException($"Tried to read Spawn, but required field {nameof(value.SpawnType)} was empty.");
            }

            if (values.Count > 5)
            {
                value.SpawnCommonality = values[5] as string;
            }

            if (values.Count > 6)
            {
                value.SpawnProbability = values[6] as string;
            }

            if (values.Count > 7)
            {
                value.EncounterCount = int.TryParse(values[7].ToString(), out var parsed) ? parsed : 0;
            }

            if (values.Count > 8)
            {
                value.IsConfirmed = bool.TryParse(values[8].ToString(), out var parsed) && parsed;
            }

            if (values.Count > 9)
            {
                value.LowestLvl = int.TryParse(values[9].ToString(), out var parsed) ? parsed : 0;
            }

            if (values.Count > 10)
            {
                value.HighestLvl = int.TryParse(values[10].ToString(), out var parsed) ? parsed : 0;
            }

            if (values.Count > 11)
            {
                value.Notes = values[11] as string;
            }

            return value;
        }
    }
}
