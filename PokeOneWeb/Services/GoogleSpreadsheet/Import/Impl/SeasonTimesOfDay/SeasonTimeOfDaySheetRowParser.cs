using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.SeasonTimesOfDay
{
    public class SeasonTimeOfDaySheetRowParser : ISheetRowParser<SeasonTimeOfDayDto>
    {
        public SeasonTimeOfDayDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 4)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new SeasonTimeOfDayDto
            {
                SeasonName = values[0] as string,
                TimeOfDayName = values[1] as string,
                StartHour = int.TryParse(values[2].ToString(), out var parsedStartHour) ? parsedStartHour : 0,
                EndHour = int.TryParse(values[3].ToString(), out var parsedEndHour) ? parsedEndHour : 0
            };

            if (value.SeasonName is null)
            {
                throw new InvalidRowDataException($"Tried to read SeasonTimeOfDay, but required field {nameof(value.SeasonName)} was empty.");
            }

            if (value.TimeOfDayName is null)
            {
                throw new InvalidRowDataException($"Tried to read SeasonTimeOfDay, but required field {nameof(value.TimeOfDayName)} was empty.");
            }

            return value;
        }
    }
}
