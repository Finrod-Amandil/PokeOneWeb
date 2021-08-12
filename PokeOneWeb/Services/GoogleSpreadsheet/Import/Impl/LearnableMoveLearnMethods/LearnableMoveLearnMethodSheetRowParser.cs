using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.LearnableMoveLearnMethods
{
    public class LearnableMoveLearnMethodSheetRowParser : ISheetRowParser<LearnableMoveLearnMethodDto>
    {
        public LearnableMoveLearnMethodDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 5)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new LearnableMoveLearnMethodDto
            {
                PokemonVarietyName = values[1] as string,
                MoveName = values[2] as string,
                LearnMethod = values[3] as string,
                IsAvailable = bool.TryParse(values[4].ToString(), out var parsedIsAvailable) && parsedIsAvailable
            };

            if (value.PokemonVarietyName is null)
            {
                throw new InvalidRowDataException($"Tried to read LearnableMoveLearnMethod, but required field {nameof(value.PokemonVarietyName)} was empty.");
            }

            if (value.MoveName is null)
            {
                throw new InvalidRowDataException($"Tried to read LearnableMoveLearnMethod, but required field {nameof(value.MoveName)} was empty.");
            }

            if (value.LearnMethod is null)
            {
                throw new InvalidRowDataException($"Tried to read LearnableMoveLearnMethod, but required field {nameof(value.LearnMethod)} was empty.");
            }

            if (values.Count > 6)
            {
                value.LevelLearnedAt = int.TryParse(values[6].ToString(), out var parsedLevelLearnedAt) ? parsedLevelLearnedAt : 0;
            }

            if (values.Count > 7)
            {
                value.RequiredItemName = values[7] as string;
            }

            if (values.Count > 8)
            {
                value.TutorName = values[8] as string;
            }

            if (values.Count > 9)
            {
                value.TutorLocation = values[9] as string;
            }

            if (values.Count > 10)
            {
                value.Comments = values[10] as string;
            }

            return value;
        }
    }
}
