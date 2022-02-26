namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.ElementalTypeRelations
{
    public class ElementalTypeRelationSheetRowParser : ISheetRowParser<ElementalTypeRelationSheetDto>
    {
        public ElementalTypeRelationSheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 3)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new ElementalTypeRelationSheetDto
            {
                AttackingTypeName = values[0] as string,
                DefendingTypeName = values[1] as string,
                Effectivity = decimal.TryParse(values[2].ToString(), out var parsed) ? parsed : 0M
            };

            if (value.AttackingTypeName is null)
            {
                throw new InvalidRowDataException($"Tried to read Elemental Type Relation, but required " +
                                                  $"field {nameof(value.AttackingTypeName)} was empty.");
            }

            if (value.DefendingTypeName is null)
            {
                throw new InvalidRowDataException($"Tried to read Elemental Type Relation, but required " +
                                                  $"field {nameof(value.DefendingTypeName)} was empty.");
            }

            return value;
        }
    }
}
