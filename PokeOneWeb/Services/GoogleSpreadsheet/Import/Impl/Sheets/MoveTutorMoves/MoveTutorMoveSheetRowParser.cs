using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.MoveTutorMoves
{
    public class MoveTutorMoveSheetRowParser : ISheetRowParser<MoveTutorMoveSheetDto>
    {
        public MoveTutorMoveSheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 2)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new MoveTutorMoveSheetDto
            {
                MoveTutorName = values[0] as string,
                MoveName = values[1] as string
            };

            if (value.MoveTutorName is null)
            {
                throw new InvalidRowDataException($"Tried to read MoveTutorMove, but required field {nameof(value.MoveTutorName)} was empty.");
            }

            if (value.MoveName is null)
            {
                throw new InvalidRowDataException($"Tried to read MoveTutorMove, but required field {nameof(value.MoveName)} was empty.");
            }

            if (values.Count > 2)
            {
                value.RedShardPrice = int.TryParse(values[2].ToString(), out var parsed) ? parsed : 0;
            }

            if (values.Count > 3)
            {
                value.BlueShardPrice = int.TryParse(values[3].ToString(), out var parsed) ? parsed : 0;
            }

            if (values.Count > 4)
            {
                value.GreenShardPrice = int.TryParse(values[4].ToString(), out var parsed) ? parsed : 0;
            }

            if (values.Count > 5)
            {
                value.YellowShardPrice = int.TryParse(values[5].ToString(), out var parsed) ? parsed : 0;
            }

            if (values.Count > 6)
            {
                value.PWTBPPrice = int.TryParse(values[6].ToString(), out var parsed) ? parsed : 0;
            }

            if (values.Count > 7)
            {
                value.BFBPPrice = int.TryParse(values[7].ToString(), out var parsed) ? parsed : 0;
            }

            if (values.Count > 8)
            {
                value.PokeDollarPrice = int.TryParse(values[8].ToString(), out var parsed) ? parsed : 0;
            }

            if (values.Count > 9)
            {
                value.PokeGoldPrice = int.TryParse(values[9].ToString(), out var parsed) ? parsed : 0;
            }

            return value;
        }
    }
}
