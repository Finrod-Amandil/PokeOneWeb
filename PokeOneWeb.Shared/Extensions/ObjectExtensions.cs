using System;
using System.Globalization;
using PokeOneWeb.Shared.Exceptions;

namespace PokeOneWeb.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static string ParseAsString(this object value)
        {
            if (value is not string stringValue)
            {
                throw new ParseException(value, "string");
            }

            return stringValue;
        }

        public static string ParseAsNonEmptyString(this object value)
        {
            var stringValue = ParseAsString(value);

            if (string.IsNullOrWhiteSpace(stringValue))
            {
                throw new ParseException(value, "non-empty string");
            }

            return stringValue;
        }

        public static string ParseAsOptionalString(this object value)
        {
            return value as string;
        }

        public static decimal ParseAsDecimal(this object value, decimal? defaultValue = null)
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

        public static decimal? ParseAsOptionalDecimal(this object value)
        {
            return decimal.TryParse(value as string, out var parsed) ? parsed : null;
        }

        public static int ParseAsInt(this object value, int? defaultValue = null)
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

        public static int? ParseAsOptionalInt(this object value)
        {
            return int.TryParse(value as string, out var parsed) ? parsed : null;
        }

        public static DateTime? ParseAsOptionalDate(this object value)
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

        public static bool ParseAsBoolean(this object value, bool? defaultValue = null)
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