using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.TimesOfDay
{
    public class TimeOfDayMapper : ISpreadsheetEntityMapper<TimeOfDaySheetDto, TimeOfDay>
    {
        private readonly ILogger _logger;

        public TimeOfDayMapper(ILogger<TimeOfDaySheetDto> logger)
        {
            _logger = logger;
        }

        public IEnumerable<TimeOfDay> Map(IDictionary<RowHash, TimeOfDaySheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning("Found invalid TimeOfDay DTO. Skipping.");
                    continue;
                }

                yield return MapTimeOfDay(dto, rowHash);
            }
        }

        public IEnumerable<TimeOfDay> MapOnto(IList<TimeOfDay> entities, IDictionary<RowHash, TimeOfDaySheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid TimeOfDay DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching TimeOfDay entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapTimeOfDay(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private static bool IsValid(TimeOfDaySheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.Name) &&
                !string.IsNullOrWhiteSpace(dto.Abbreviation);
        }

        private TimeOfDay MapTimeOfDay(TimeOfDaySheetDto dto, RowHash rowHash, TimeOfDay timeOfDay = null)
        {
            timeOfDay ??= new TimeOfDay();

            timeOfDay.IdHash = rowHash.IdHash;
            timeOfDay.Hash = rowHash.ContentHash;
            timeOfDay.ImportSheetId = rowHash.ImportSheetId;
            timeOfDay.SortIndex = dto.SortIndex;
            timeOfDay.Name = dto.Name;
            timeOfDay.Abbreviation = dto.Abbreviation;
            timeOfDay.Color = dto.Color;

            return timeOfDay;
        }
    }
}
