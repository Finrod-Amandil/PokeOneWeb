using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.ElementalTypeRelations
{
    public class ElementalTypeRelationSheetRowParser : ISheetRowParser<ElementalTypeRelationDto>
    {
        public ElementalTypeRelationDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 3)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new ElementalTypeRelationDto
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
