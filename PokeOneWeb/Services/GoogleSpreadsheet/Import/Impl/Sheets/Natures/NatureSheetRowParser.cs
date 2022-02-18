using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Natures
{
    public class NatureSheetRowParser : ISheetRowParser<NatureSheetDto>
    {
        public NatureSheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 6)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new NatureSheetDto
            {
                Name = values[0] as string,
                Attack = int.TryParse(values[1].ToString(), out var parsedAtk) ? parsedAtk : 0,
                SpecialAttack = int.TryParse(values[2].ToString(), out var parsedSpa) ? parsedSpa : 0,
                Defense = int.TryParse(values[3].ToString(), out var parsedDef) ? parsedDef : 0,
                SpecialDefense = int.TryParse(values[4].ToString(), out var parsedSpd) ? parsedSpd : 0,
                Speed = int.TryParse(values[5].ToString(), out var parsedSpe) ? parsedSpe : 0
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read Nature, but required field {nameof(value.Name)} was empty.");
            }

            return value;
        }
    }
}
