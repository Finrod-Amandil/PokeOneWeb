using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Availabilities
{
    public class AvailabilitySheetRowParser : ISheetRowParser<AvailabilitySheetDto>
    {
        public AvailabilitySheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 2)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new AvailabilitySheetDto
            {
                Name = values[0] as string,
                Description = values[1] as string
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read Availability, but required field {nameof(value.Name)} was empty.");
            }

            if (value.Description is null)
            {
                throw new InvalidRowDataException($"Tried to read Availability, but required field {nameof(value.Description)} was empty.");
            }

            return value;
        }
    }
}
