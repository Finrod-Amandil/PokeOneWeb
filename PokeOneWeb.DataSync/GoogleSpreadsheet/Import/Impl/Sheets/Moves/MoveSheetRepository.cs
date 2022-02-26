using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Moves
{
    public class MoveSheetRepository : SheetRepository<MoveSheetDto, Move>
    {
        public MoveSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<MoveSheetDto> parser,
            ISpreadsheetEntityMapper<MoveSheetDto, Move> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter) { }

        protected override DbSet<Move> DbSet => DbContext.Moves;

        protected override Entity Entity => Entity.Move;

        protected override List<Move> AttachRelatedEntities(List<Move> entities)
        {
            var damageClasses = DbContext.MoveDamageClasses.ToList();
            var types = DbContext.ElementalTypes.ToList();

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var damageClass = damageClasses.SingleOrDefault(c => c.Name.EqualsExact(entity.DamageClass.Name));

                if (damageClass is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching MoveDamageClass {entity.DamageClass.Name}, skipping move.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.DamageClassId = damageClass.Id;
                entity.DamageClass = damageClass;

                var type = types.SingleOrDefault(t => t.Name.EqualsExact(entity.ElementalType.Name));

                if (type is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching ElementalType {entity.ElementalType.Name}, skipping move.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.ElementalTypeId = type.Id;
                entity.ElementalType = type;
            }

            return entities;
        }
    }
}
