using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Currencies
{
    public class CurrencyMapper : XSpreadsheetEntityMapper<CurrencySheetDto, Currency>
    {
        public CurrencyMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Entity Entity => Entity.Currency;

        protected override bool IsValid(CurrencySheetDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.ItemName);
        }

        protected override string GetUniqueName(CurrencySheetDto dto)
        {
            return dto.ItemName;
        }

        protected override Currency MapEntity(CurrencySheetDto dto, RowHash rowHash, Currency currency = null)
        {
            currency ??= new Currency();

            currency.IdHash = rowHash.IdHash;
            currency.Hash = rowHash.Hash;
            currency.ImportSheetId = rowHash.ImportSheetId;
            currency.Item = new Item { Name = dto.ItemName };

            return currency;
        }
    }
}