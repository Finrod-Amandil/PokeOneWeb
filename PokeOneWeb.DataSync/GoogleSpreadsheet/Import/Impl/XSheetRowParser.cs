using System;
using System.Collections.Generic;
using System.Globalization;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Exceptions;
using PokeOneWeb.Shared.Exceptions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl
{
    public abstract class XSheetRowParser<TDto>
        : XISheetRowParser<TDto> where TDto : XISpreadsheetEntityDto, new()
    {
        public TDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < RequiredValueCount)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            if (values.Count > MappingDelegates.Count)
            {
                throw new InvalidRowDataException("Row data does contain too many values");
            }

            var dto = new TDto();

            for (var i = 0; i < values.Count; i++)
            {
                MappingDelegates[i](dto, values[i]);
            }

            return dto;
        }

        protected abstract int RequiredValueCount { get; }

        protected abstract List<Action<TDto, object>> MappingDelegates { get; }

        protected static string ParseAsString(object value)
        {
            if (value is not string stringValue)
            {
                throw new ParseException(value, "string");
            }

            return stringValue;
        }

        protected static string ParseAsNonEmptyString(object value)
        {
            var stringValue = ParseAsString(value);

            if (string.IsNullOrWhiteSpace(stringValue))
            {
                throw new ParseException(value, "non-empty string");
            }

            return stringValue;
        }

        protected static decimal ParseAsDecimal(object value, decimal? defaultValue = null)
        {
            var stringValue = value.ToString();

            if (string.IsNullOrWhiteSpace(stringValue) && defaultValue is not null)
            {
                return (decimal)defaultValue;
            }

            var canParse = decimal.TryParse(stringValue, out var parsed);

            if (!canParse)
            {
                throw new ParseException(value, "decimal");
            }

            return parsed;
        }

        protected static int ParseAsInt(object value, int? defaultValue = null)
        {
            var stringValue = value.ToString();

            if (string.IsNullOrWhiteSpace(stringValue) && defaultValue is not null)
            {
                return (int)defaultValue;
            }

            var canParse = int.TryParse(value.ToString(), out var parsed);

            if (!canParse)
            {
                throw new ParseException(value, "int");
            }

            return parsed;
        }

        protected static DateTime? ParseAsOptionalDate(object value)
        {
            if (string.IsNullOrWhiteSpace(value.ToString()))
            {
                return null;
            }

            var format = "dd.MM.yyyy";
            var canParse = DateTime.TryParseExact(
                value.ToString(),
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                out var parsed);

            if (!canParse)
            {
                throw new ParseException(value, "DateTime");
            }

            return parsed;
        }

        protected static bool ParseAsBoolean(object value, bool? defaultValue = null)
        {
            var stringValue = value.ToString();

            if (string.IsNullOrWhiteSpace(stringValue) && defaultValue is not null)
            {
                return (bool)defaultValue;
            }

            var canParse = bool.TryParse(stringValue, out var parsed);

            if (!canParse)
            {
                throw new ParseException(value, "bool");
            }

            return parsed;
        }
    }
}