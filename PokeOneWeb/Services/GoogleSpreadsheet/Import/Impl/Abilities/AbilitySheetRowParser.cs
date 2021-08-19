using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Abilities
{
    public class AbilitySheetRowParser : ISheetRowParser<AbilitySheetDto>
    {
        public AbilitySheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 1)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new AbilitySheetDto
            {
                Name = values[0] as string
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read Ability, but required field {nameof(value.Name)} was empty.");
            }

            if (values.Count > 1)
            {
                value.PokeApiName = values[1] as string;
            }

            if (values.Count > 2)
            {
                value.ShortEffect = values[2] as string;
            }

            if (values.Count > 3)
            {
                value.Effect = values[3] as string;
            }

            return value;
        }
    }
}
