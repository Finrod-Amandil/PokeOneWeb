using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveTutors
{
    public class MoveTutorSheetRepository : SheetRepository<MoveTutorSheetDto, MoveTutor>
    {
        public MoveTutorSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<MoveTutorSheetDto> parser,
            ISpreadsheetEntityMapper<MoveTutorSheetDto, MoveTutor> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter) { }

        protected override DbSet<MoveTutor> DbSet => DbContext.MoveTutors;

        protected override Entity Entity => Entity.MoveTutor;

        protected override List<MoveTutor> AttachRelatedEntities(List<MoveTutor> entities)
        {
            var locations = DbContext.Locations.ToList();

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var location = locations.SingleOrDefault(l => l.Name.EqualsExact(entity.Location.Name));

                if (location is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching Location {entity.Location.Name}, skipping move tutor.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.LocationId = location.Id;
                entity.Location = location;
            }

            return entities;
        }
    }
}
