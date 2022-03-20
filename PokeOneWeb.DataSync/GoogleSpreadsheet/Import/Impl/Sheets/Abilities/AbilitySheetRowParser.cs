using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Abilities
{
    public class AbilitySheetRowParser : ISheetRowParser<AbilitySheetDto>
    {
        public AbilitySheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 1)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new AbilitySheetDto
            {
                Name = values[0] as string
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read Ability, but required field {nameof(value.Name)} was empty.");
            }

            if (values.Count > 1)
            {
                value.ShortEffect = values[1] as string;
            }

            if (values.Count > 2)
            {
                value.Effect = values[2] as string;
            }

            if (values.Count > 3)
            {
                value.AtkBoost = decimal.TryParse(values[3].ToString(), out var parsed) ? parsed : 1M;
            }

            if (values.Count > 4)
            {
                value.SpaBoost = decimal.TryParse(values[4].ToString(), out var parsed) ? parsed : 1M;
            }

            if (values.Count > 5)
            {
                value.DefBoost = decimal.TryParse(values[5].ToString(), out var parsed) ? parsed : 1M;
            }

            if (values.Count > 6)
            {
                value.SpdBoost = decimal.TryParse(values[6].ToString(), out var parsed) ? parsed : 1M;
            }

            if (values.Count > 7)
            {
                value.SpeBoost = decimal.TryParse(values[7].ToString(), out var parsed) ? parsed : 1M;
            }

            if (values.Count > 8)
            {
                value.HpBoost = decimal.TryParse(values[8].ToString(), out var parsed) ? parsed : 1M;
            }

            if (values.Count > 9)
            {
                value.BoostConditions = values[9] as string;
            }

            return value;
        }
    }
}
