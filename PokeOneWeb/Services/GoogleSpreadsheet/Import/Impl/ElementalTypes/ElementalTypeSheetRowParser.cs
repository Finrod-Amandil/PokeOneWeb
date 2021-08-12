using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.ElementalTypes
{
    public class ElementalTypeSheetRowParser : ISheetRowParser<ElementalTypeDto>
    {
        public ElementalTypeDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 1)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new ElementalTypeDto
            {
                Name = values[0] as string
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read ElementalType, but required field {nameof(value.Name)} was empty.");
            }

            if (values.Count > 1)
            {
                value.PokeApiName = values[1] as string;
            }

            return value;
        }
    }
}
