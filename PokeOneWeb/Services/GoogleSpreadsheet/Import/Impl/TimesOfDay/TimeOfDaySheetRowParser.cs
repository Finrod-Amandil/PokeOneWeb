﻿using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.TimesOfDay
{
    public class TimeOfDaySheetRowParser : ISheetRowParser<TimeOfDayDto>
    {
        public TimeOfDayDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 3)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new TimeOfDayDto
            {
                SortIndex = int.TryParse(values[0].ToString(), out var parsed) ? parsed : 0,
                Name = values[1] as string,
                Abbreviation = values[2] as string
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read TimeOfDay, but required field {nameof(value.Name)} was empty.");
            }

            if (value.Abbreviation is null)
            {
                throw new InvalidRowDataException($"Tried to read TimeOfDay, but required field {nameof(value.Abbreviation)} was empty.");
            }

            if (values.Count > 3)
            {
                value.Color = values[3] as string;
            }

            return value;
        }
    }
}
