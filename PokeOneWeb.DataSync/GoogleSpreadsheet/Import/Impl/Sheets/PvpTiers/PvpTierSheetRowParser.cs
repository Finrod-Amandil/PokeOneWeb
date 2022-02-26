namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.PvpTiers
{
    public class PvpTierSheetRowParser : ISheetRowParser<PvpTierSheetDto>
    {
        public PvpTierSheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 2)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new PvpTierSheetDto
            {
                Name = values[0] as string,
                SortIndex = int.TryParse(values[1].ToString(), out var parsed) ? parsed : 0
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read PvpTier, but required field {nameof(value.Name)} was empty.");
            }

            return value;
        }
    }
}
