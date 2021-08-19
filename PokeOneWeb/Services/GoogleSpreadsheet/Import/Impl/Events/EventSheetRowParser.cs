using System;
using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Events
{
    public class EventSheetRowParser : ISheetRowParser<EventSheetDto>
    {
        public EventSheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 1)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new EventSheetDto
            {
                Name = values[0] as string
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read Event, but required field {nameof(value.Name)} was empty.");
            }

            if (values.Count > 1)
            {
                var dateTimeString = values[1] as string;
                var canParse = DateTime.TryParse(dateTimeString, out var eventStart);

                if (!canParse && !string.IsNullOrWhiteSpace(dateTimeString))
                {
                    throw new InvalidRowDataException($"Failed to parse date {dateTimeString}.");
                }

                value.StartDate = eventStart;
            }

            if (values.Count > 2)
            {
                var dateTimeString = values[2] as string;
                var canParse = DateTime.TryParse(dateTimeString, out var eventEnd);

                if (!canParse && !string.IsNullOrWhiteSpace(dateTimeString))
                {
                    throw new InvalidRowDataException($"Failed to parse date {dateTimeString}.");
                }

                value.EndDate = eventEnd;
            }

            return value;
        }
    }
}
