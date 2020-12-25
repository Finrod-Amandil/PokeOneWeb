using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Moves
{
    public class MoveReader : SpreadsheetEntityReader<MoveDto>
    {
        public MoveReader(ILogger<MoveReader> logger) : base(logger) { }

        protected override MoveDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 9)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new MoveDto
            {
                Name = rowData.Values[0]?.EffectiveValue?.StringValue,
                DoInclude = rowData.Values[1]?.EffectiveValue?.BoolValue ?? false,
                ResourceName = rowData.Values[2]?.EffectiveValue?.StringValue,
                DamageClassName = rowData.Values[3]?.EffectiveValue?.StringValue,
                TypeName = rowData.Values[4]?.EffectiveValue?.StringValue,
                AttackPower = (int?) rowData.Values[5]?.EffectiveValue?.NumberValue ?? 0,
                Accuracy = (int?) rowData.Values[6]?.EffectiveValue?.NumberValue ?? 0,
                PowerPoints = (int?) rowData.Values[7]?.EffectiveValue?.NumberValue ?? 0,
                Priority = (int?) rowData.Values[8]?.EffectiveValue?.NumberValue ?? 0
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read Move, but required field {nameof(value.Name)} was empty.");
            }

            if (value.ResourceName is null)
            {
                throw new InvalidRowDataException($"Tried to read Move, but required field {nameof(value.ResourceName)} was empty.");
            }

            if (value.DamageClassName is null)
            {
                throw new InvalidRowDataException($"Tried to read Move, but required field {nameof(value.DamageClassName)} was empty.");
            }

            if (value.TypeName is null)
            {
                throw new InvalidRowDataException($"Tried to read Move, but required field {nameof(value.TypeName)} was empty.");
            }

            if (rowData.Values.Count > 9)
            {
                value.PokeApiName = rowData.Values[9]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 10)
            {
                value.Effect = rowData.Values[10]?.EffectiveValue?.StringValue;
            }

            return value;
        }
    }
}
