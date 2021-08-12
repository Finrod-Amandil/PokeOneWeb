using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.SeasonTimesOfDay
{
    public class SeasonTimeOfDayMapper : ISpreadsheetEntityMapper<SeasonTimeOfDayDto, SeasonTimeOfDay>
    {
        private readonly ILogger<SeasonTimeOfDayMapper> _logger;

        private IDictionary<string, TimeOfDay> _timesOfDay;
        private IDictionary<string, Season> _seasons;

        public SeasonTimeOfDayMapper(ILogger<SeasonTimeOfDayMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<SeasonTimeOfDay> Map(IDictionary<RowHash, SeasonTimeOfDayDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _seasons = new Dictionary<string, Season>();
            _timesOfDay = new Dictionary<string, TimeOfDay>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid SeasonTimeOfDay DTO. Skipping.");
                    continue;
                }

                yield return MapSeasonTimeOfDay(dto, rowHash);
            }
        }

        public IEnumerable<SeasonTimeOfDay> MapOnto(IList<SeasonTimeOfDay> entities, IDictionary<RowHash, SeasonTimeOfDayDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _seasons = new Dictionary<string, Season>();
            _timesOfDay = new Dictionary<string, TimeOfDay>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid SeasonTimeOfDay DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching SeasonTimeOfDay entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapSeasonTimeOfDay(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private bool IsValid(SeasonTimeOfDayDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.SeasonName) &&
                !string.IsNullOrWhiteSpace(dto.TimeOfDayName);
        }

        private SeasonTimeOfDay MapSeasonTimeOfDay(
            SeasonTimeOfDayDto dto,
            RowHash rowHash,
            SeasonTimeOfDay seasonTimeOfDay = null)
        {
            seasonTimeOfDay ??= new SeasonTimeOfDay();

            Season season;
            if (_seasons.ContainsKey(dto.SeasonName))
            {
                season = _seasons[dto.SeasonName];
            }
            else
            {
                season = new Season { Name = dto.SeasonName };
                _seasons.Add(dto.SeasonName, season);
            }

            TimeOfDay timeOfDay;
            if (_timesOfDay.ContainsKey(dto.TimeOfDayName))
            {
                timeOfDay = _timesOfDay[dto.TimeOfDayName];
            }
            else
            {
                timeOfDay = new TimeOfDay { Name = dto.TimeOfDayName };
                _timesOfDay.Add(dto.TimeOfDayName, timeOfDay);
            }

            seasonTimeOfDay.IdHash = rowHash.IdHash;
            seasonTimeOfDay.Hash = rowHash.ContentHash;
            seasonTimeOfDay.ImportSheetId = rowHash.ImportSheetId;
            seasonTimeOfDay.StartHour = dto.StartHour;
            seasonTimeOfDay.EndHour = dto.EndHour;
            seasonTimeOfDay.Season = season;
            seasonTimeOfDay.TimeOfDay = timeOfDay;

            return seasonTimeOfDay;
        }
    }
}
