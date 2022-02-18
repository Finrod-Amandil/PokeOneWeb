using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.ElementalTypeRelations
{
    public class ElementalTypeRelationSheetRepository : 
        SheetRepository<ElementalTypeRelationSheetDto, ElementalTypeRelation>
    {
        public ElementalTypeRelationSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<ElementalTypeRelationSheetDto> parser,
            ISpreadsheetEntityMapper<ElementalTypeRelationSheetDto, ElementalTypeRelation> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter) { }

        protected override DbSet<ElementalTypeRelation> DbSet => DbContext.ElementalTypeRelations;

        protected override Entity Entity => Entity.ElementalTypeRelation;

        protected override List<ElementalTypeRelation> AttachRelatedEntities(List<ElementalTypeRelation> entities)
        {
            var types = DbContext.ElementalTypes.ToList();

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var attackingType = types.SingleOrDefault(t =>
                    t.Name.Equals(entity.AttackingType.Name, StringComparison.Ordinal));

                if (attackingType is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"No unique matching type could be found for attacking type" +
                        $" {entity.AttackingType.Name}. Skipping.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                var defendingType = types.SingleOrDefault(t =>
                    t.Name.Equals(entity.DefendingType.Name, StringComparison.Ordinal));

                if (defendingType is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"No unique matching type could be found for defending type" +
                        $" {entity.DefendingType.Name}. Skipping.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.AttackingTypeId = attackingType.Id;
                entity.AttackingType = attackingType;
                entity.DefendingTypeId = defendingType.Id;
                entity.DefendingType = defendingType;
            }

            return entities;
        }
    }
}
