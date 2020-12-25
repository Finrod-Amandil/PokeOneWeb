using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.ElementalTypeRelations
{
    public class ElementalTypeRelationReader : SpreadsheetEntityReader<ElementalTypeRelationDto>
    {
        public ElementalTypeRelationReader(ILogger<ElementalTypeRelationReader> logger) : base(logger) { }

        protected override ElementalTypeRelationDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 3)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new ElementalTypeRelationDto
            {
                AttackingTypeName = rowData.Values[0]?.EffectiveValue?.StringValue,
                DefendingTypeName = rowData.Values[1]?.EffectiveValue?.StringValue,
                Effectivity = (decimal?) rowData.Values[2]?.EffectiveValue?.NumberValue ?? 0
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
