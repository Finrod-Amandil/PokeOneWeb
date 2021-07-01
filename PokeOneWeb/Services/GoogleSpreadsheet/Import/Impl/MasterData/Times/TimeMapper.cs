using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Times
{
    public class TimeMapper : ISpreadsheetEntityMapper<TimeDto, SeasonTimeOfDay>
    {
        private readonly ILogger<TimeMapper> _logger;

        private IDictionary<string, TimeOfDay> _times;
        private IDictionary<string, Season> _seasons;

        public TimeMapper(ILogger<TimeMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<SeasonTimeOfDay> Map(IEnumerable<TimeDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            _times = new Dictionary<string, TimeOfDay>();
            _seasons = new Dictionary<string, Season>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Time DTO. Skipping.");
                    continue;
                }

                TimeOfDay time;
                if (_times.ContainsKey(dto.TimeName))
                {
                    time = _times[dto.TimeName];
                }
                else
                {
                    time = new TimeOfDay
                    {
                        SortIndex = dto.TimeSortIndex,
                        Name = dto.TimeName,
                        Abbreviation = dto.TimeAbbreviation,
                        Color = dto.TimeColor
                    };
                    _times.Add(dto.TimeName, time);
                }

                Season season;
                if (_seasons.ContainsKey(dto.SeasonName))
                {
                    season = _seasons[dto.SeasonName];
                }
                else
                {
                    season = new Season
                    {
                        SortIndex = dto.SeasonSortIndex,
                        Name = dto.SeasonName,
                        Abbreviation = dto.SeasonAbbreviation,
                        Color = dto.SeasonColor
                    };
                    _seasons.Add(dto.SeasonName, season);
                }

                yield return new SeasonTimeOfDay
                {
                    Season = season,
                    TimeOfDay = time,
                    StartHour = dto.StartHour,
                    EndHour = dto.EndHour
                };
            }
        }

        private bool IsValid(TimeDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.SeasonName) &&
                !string.IsNullOrWhiteSpace(dto.SeasonAbbreviation) &&
                !string.IsNullOrWhiteSpace(dto.TimeName) &&
                !string.IsNullOrWhiteSpace(dto.SeasonAbbreviation);
        }
    }
}
