namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.BagCategories
{
    public class BagCategorySheetRowParser : ISheetRowParser<BagCategorySheetDto>
    {
        public BagCategorySheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 1)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new BagCategorySheetDto
            {
                Name = values[0] as string,
                SortIndex = int.TryParse(values[1].ToString(), out var parsed) ? parsed : 0
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read BagCategory, but required field {nameof(value.Name)} was empty.");
            }

            return value;
        }
    }
}
