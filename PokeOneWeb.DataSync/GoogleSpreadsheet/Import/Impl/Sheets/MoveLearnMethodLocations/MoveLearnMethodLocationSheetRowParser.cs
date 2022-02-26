namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveLearnMethodLocations
{
    public class MoveLearnMethodLocationSheetRowParser : ISheetRowParser<MoveLearnMethodLocationSheetDto>
    {
        public MoveLearnMethodLocationSheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 4)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new MoveLearnMethodLocationSheetDto
            {
                MoveLearnMethodName = values[0] as string,
                TutorType = values[1] as string,
                NpcName = values[2] as string,
                LocationName = values[3] as string,
            };

            if (value.MoveLearnMethodName is null)
            {
                throw new InvalidRowDataException($"Tried to read MoveLearnMethodLocation, " +
                                                  $"but required field {nameof(value.MoveLearnMethodName)} was empty.");
            }

            if (value.LocationName is null)
            {
                throw new InvalidRowDataException($"Tried to read MoveLearnMethodLocation, " +
                                                  $"but required field {nameof(value.LocationName)} was empty.");
            }

            if (values.Count > 4)
            {
                value.PlacementDescription = values[4] as string;
            }

            if (values.Count > 5)
            {
                value.PokeDollarPrice = int.TryParse(values[5].ToString(), out var parsed) ? parsed : 0;
            }

            if (values.Count > 6)
            {
                value.PokeGoldPrice = int.TryParse(values[6].ToString(), out var parsed) ? parsed : 0;
            }

            if (values.Count > 7)
            {
                value.BigMushroomPrice = int.TryParse(values[7].ToString(), out var parsed) ? parsed : 0;
            }

            if (values.Count > 8)
            {
                value.HeartScalePrice = int.TryParse(values[8].ToString(), out var parsed) ? parsed : 0;
            }

            return value;
        }
    }
}
