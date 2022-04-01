using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.SeasonTimesOfDay
{
    public class SeasonTimeOfDayMapper : SpreadsheetEntityMapper<SeasonTimeOfDaySheetDto, SeasonTimeOfDay>
    {
        private readonly Dictionary<string, TimeOfDay> _timesOfDay = new();
        private readonly Dictionary<string, Season> _seasons = new();

        public SeasonTimeOfDayMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Entity Entity => Entity.SeasonTimeOfDay;

        protected override bool IsValid(SeasonTimeOfDaySheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.SeasonName) &&
                !string.IsNullOrWhiteSpace(dto.TimeOfDayName);
        }

        protected override string GetUniqueName(SeasonTimeOfDaySheetDto dto)
        {
            return dto.TimeOfDayName + dto.SeasonName;
        }

        protected override SeasonTimeOfDay MapEntity(
            SeasonTimeOfDaySheetDto dto,
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