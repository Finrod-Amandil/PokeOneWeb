﻿using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.PvpTiers
{
    public class PvpTierSheetRowParser : ISheetRowParser<PvpTierDto>
    {
        public PvpTierDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 2)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new PvpTierDto
            {
                Name = values[0] as string,
                SortIndex = int.TryParse(values[1].ToString(), out var parsed) ? parsed : 0
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read PvpTier, but required field {nameof(value.Name)} was empty.");
            }

            return value;
        }
    }
}
