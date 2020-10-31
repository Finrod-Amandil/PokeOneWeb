using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Impl.TutorMoves
{
    public class TutorMoveReader : SpreadsheetReader<TutorMoveDto>
    {
        public TutorMoveReader(ILogger<TutorMoveReader> logger) : base(logger) { }

        protected override TutorMoveDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 4)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new TutorMoveDto
            {
                TutorType = rowData.Values[0]?.EffectiveValue?.StringValue,
                TutorName = rowData.Values[1]?.EffectiveValue?.StringValue,
                LocationName = rowData.Values[2]?.EffectiveValue?.StringValue,
                PlacementDescription = rowData.Values[3]?.EffectiveValue?.StringValue,
                MoveName = null,
                RedShardPrice = 0,
                BlueShardPrice = 0,
                GreenShardPrice = 0,
                YellowShardPrice = 0,
                PWTBPPrice = 0,
                BFBPPrice = 0,
                PokeDollarPrice = 0,
                PokeGoldPrice = 0,
                BigMushrooms = 0,
                HeartScales = 0
            };

            if (value.TutorType is null)
            {
                throw new InvalidRowDataException($"Tried to read TutorMove, but required field {nameof(value.TutorType)} was empty.");
            }

            if (value.TutorName is null)
            {
                throw new InvalidRowDataException($"Tried to read TutorMove, but required field {nameof(value.TutorName)} was empty.");
            }

            if (value.LocationName is null)
            {
                throw new InvalidRowDataException($"Tried to read TutorMove, but required field {nameof(value.LocationName)} was empty.");
            }

            if (rowData.Values.Count > 4)
            {
                value.MoveName = rowData.Values[4]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 5)
            {
                value.RedShardPrice = (int) (rowData.Values[5]?.EffectiveValue?.NumberValue ?? 0);
            }

            if (rowData.Values.Count > 6)
            {
                value.BlueShardPrice = (int)(rowData.Values[6]?.EffectiveValue?.NumberValue ?? 0);
            }

            if (rowData.Values.Count > 7)
            {
                value.GreenShardPrice = (int)(rowData.Values[7]?.EffectiveValue?.NumberValue ?? 0);
            }

            if (rowData.Values.Count > 8)
            {
                value.YellowShardPrice = (int)(rowData.Values[8]?.EffectiveValue?.NumberValue ?? 0);
            }

            if (rowData.Values.Count > 9)
            {
                value.PWTBPPrice = (int)(rowData.Values[9]?.EffectiveValue?.NumberValue ?? 0);
            }

            if (rowData.Values.Count > 10)
            {
                value.BFBPPrice = (int)(rowData.Values[10]?.EffectiveValue?.NumberValue ?? 0);
            }

            if (rowData.Values.Count > 11)
            {
                value.PokeDollarPrice = (int)(rowData.Values[11]?.EffectiveValue?.NumberValue ?? 0);
            }

            if (rowData.Values.Count > 12)
            {
                value.PokeGoldPrice = (int)(rowData.Values[12]?.EffectiveValue?.NumberValue ?? 0);
            }

            if (rowData.Values.Count > 13)
            {
                value.BigMushrooms = (int)(rowData.Values[13]?.EffectiveValue?.NumberValue ?? 0);
            }

            if (rowData.Values.Count > 14)
            {
                value.HeartScales = (int)(rowData.Values[14]?.EffectiveValue?.NumberValue ?? 0);
            }

            return value;
        }
    }
}
