using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MoveDamageClasses
{
    public class MoveDamageClassSheetRowParser : ISheetRowParser<MoveDamageClassSheetDto>
    {
        public MoveDamageClassSheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 1)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new MoveDamageClassSheetDto
            {
                Name = values[0] as string
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read MoveDamageClass, but required field {nameof(value.Name)} was empty.");
            }

            return value;
        }
    }
}
