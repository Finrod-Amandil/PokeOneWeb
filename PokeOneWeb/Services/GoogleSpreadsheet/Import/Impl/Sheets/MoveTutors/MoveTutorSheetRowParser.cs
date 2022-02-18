using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.MoveTutors
{
    public class MoveTutorSheetRowParser : ISheetRowParser<MoveTutorSheetDto>
    {
        public MoveTutorSheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 2)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new MoveTutorSheetDto
            {
                Name = values[0] as string,
                LocationName = values[1] as string
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read MoveTutor, but required field {nameof(value.Name)} was empty.");
            }

            if (value.LocationName is null)
            {
                throw new InvalidRowDataException($"Tried to read MoveTutor, but required field {nameof(value.LocationName)} was empty.");
            }

            if (values.Count > 2)
            {
                value.PlacementDescription = values[2] as string;
            }

            return value;
        }
    }
}
