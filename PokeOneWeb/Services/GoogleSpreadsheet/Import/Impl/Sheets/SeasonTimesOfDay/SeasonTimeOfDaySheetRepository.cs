using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.SeasonTimesOfDay
{
    public class SeasonTimeOfDaySheetRepository : SheetRepository<SeasonTimeOfDaySheetDto, SeasonTimeOfDay>
    {
        public SeasonTimeOfDaySheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<SeasonTimeOfDaySheetDto> parser,
            ISpreadsheetEntityMapper<SeasonTimeOfDaySheetDto, SeasonTimeOfDay> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter) { }

        protected override DbSet<SeasonTimeOfDay> DbSet => DbContext.SeasonTimesOfDay;

        protected override Entity Entity => Entity.SeasonTimeOfDay;

        protected override List<SeasonTimeOfDay> AttachRelatedEntities(List<SeasonTimeOfDay> entities)
        {
            var seasons = DbContext.Seasons.ToList();
            var timesOfDay = DbContext.TimesOfDay.ToList();

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var season = seasons.SingleOrDefault(s => s.Name.EqualsExact(entity.Season.Name));

                if (season is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching Season {entity.Season.Name}, skipping season time of day.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.SeasonId = season.Id;
                entity.Season = season;

                var timeOfDay = timesOfDay.SingleOrDefault(t => t.Name.EqualsExact(entity.TimeOfDay.Name));

                if (timeOfDay is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching TimeOfDay {entity.TimeOfDay.Name}, skipping season time of day.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.TimeOfDayId = timeOfDay.Id;
                entity.TimeOfDay = timeOfDay;
            }

            return entities;
        }
    }
}
