namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Currencies
{
    public class CurrencySheetRowParser : SheetRowParser<CurrencySheetDto>
    {
        protected override int RequiredValueCount => 1;

        protected override List<Action<CurrencySheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.ItemName = ParseAsNonEmptyString(value)
        };
    }
}
