using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Currencies
{
    public class CurrencySheetRowParser : ISheetRowParser<CurrencySheetDto>
    {
        public CurrencySheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 1)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new CurrencySheetDto
            {
                ItemName = values[0] as string
            };

            if (value.ItemName is null)
            {
                throw new InvalidRowDataException($"Tried to read Currency, but required field {nameof(value.ItemName)} was empty.");
            }

            return value;
        }
    }
}
