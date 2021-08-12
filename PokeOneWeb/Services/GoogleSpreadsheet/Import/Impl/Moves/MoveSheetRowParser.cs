using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Moves
{
    public class MoveSheetRowParser : ISheetRowParser<MoveDto>
    {
        public MoveDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 9)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new MoveDto
            {
                Name = values[0] as string,
                DoInclude = bool.TryParse(values[1].ToString(), out var parsedDoInclude) && parsedDoInclude,
                ResourceName = values[2] as string,
                DamageClassName = values[3] as string,
                TypeName = values[4] as string,
                AttackPower = int.TryParse(values[5].ToString(), out var parsedAtk) ? parsedAtk : 0,
                Accuracy = int.TryParse(values[6].ToString(), out var parsedAccuracy) ? parsedAccuracy : 0,
                PowerPoints = int.TryParse(values[7].ToString(), out var parsedPowerPoints) ? parsedPowerPoints : 0,
                Priority = int.TryParse(values[8].ToString(), out var parsedPriority) ? parsedPriority : 0
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

            if (values.Count > 9)
            {
                value.PokeApiName = values[9] as string;
            }

            if (values.Count > 10)
            {
                value.Effect = values[10] as string;
            }

            return value;
        }
    }
}
