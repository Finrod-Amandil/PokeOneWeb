using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Builds
{
    public class BuildSheetRowParser : ISheetRowParser<BuildSheetDto>
    {
        public BuildSheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 1)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new BuildSheetDto
            {
                PokemonVarietyName = values[0] as string
            };

            if (value.PokemonVarietyName is null)
            {
                throw new InvalidRowDataException($"Tried to read Build, but required field {nameof(value.PokemonVarietyName)} was empty.");
            }

            if (values.Count > 1)
            {
                value.BuildName = values[1] as string;
            }

            if (values.Count > 2)
            {
                value.BuildDescription = values[2] as string;
            }

            if (values.Count > 3)
            {
                value.Move1 = values[3] as string;
            }

            if (values.Count > 4)
            {
                value.Move2 = values[4] as string;
            }

            if (values.Count > 5)
            {
                value.Move3 = values[5] as string;
            }

            if (values.Count > 6)
            {
                value.Move4 = values[6] as string;
            }

            if (values.Count > 7)
            {
                value.Item = values[7] as string;
            }

            if (values.Count > 8)
            {
                value.Nature = values[8] as string;
            }

            if (values.Count > 9)
            {
                value.Ability = values[9] as string;
            }

            if (values.Count > 10)
            {
                value.EvDistribution = values[10] as string;
            }

            return value;
        }
    }
}
