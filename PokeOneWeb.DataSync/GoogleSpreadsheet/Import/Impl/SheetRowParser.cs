namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl
{
    public abstract class SheetRowParser<TDto> 
        : ISheetRowParser<TDto> where TDto : ISpreadsheetEntityDto, new()
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

        protected static decimal ParseAsDecimal(object value)
        {
            var canParse = decimal.TryParse(value.ToString(), out var parsed);

            if (!canParse)
            {
                throw new UnparsableRowValueException(value, "decimal");
            }

            return parsed;
        }

        protected static string ParseAsString(object value)
        {
            if (value is not string stringValue)
            {
                throw new UnparsableRowValueException(value, "string");
            }

            return stringValue;
        }

        protected static string ParseAsNonEmptyString(object value)
        {
            var stringValue = ParseAsString(value);

            if (string.IsNullOrWhiteSpace(stringValue))
            {
               throw new UnparsableRowValueException(value, "non-empty string");
            }

            return stringValue;
        }
    }
}
