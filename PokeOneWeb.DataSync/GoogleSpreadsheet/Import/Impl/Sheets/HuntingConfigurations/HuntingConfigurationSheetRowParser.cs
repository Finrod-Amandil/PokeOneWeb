using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.HuntingConfigurations
{
    public class HuntingConfigurationSheetRowParser : ISheetRowParser<HuntingConfigurationSheetDto>
    {
        public HuntingConfigurationSheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 3)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new HuntingConfigurationSheetDto
            {
                PokemonVarietyName = values[0] as string,
                Nature = values[1] as string,
                Ability = values[2] as string
            };

            if (value.PokemonVarietyName is null)
            {
                throw new InvalidRowDataException($"Tried to read Hunting Configuration, but required field {nameof(value.PokemonVarietyName)} was empty.");
            }

            if (value.Nature is null)
            {
                throw new InvalidRowDataException($"Tried to read Hunting Configuration, but required field {nameof(value.Nature)} was empty.");
            }

            if (value.Ability is null)
            {
                throw new InvalidRowDataException($"Tried to read Hunting Configuration, but required field {nameof(value.Ability)} was empty.");
            }

            return value;
        }
    }
}