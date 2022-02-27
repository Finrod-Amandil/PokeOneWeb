﻿using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Currencies
{
    public class CurrencySheetRepository : SheetRepository<CurrencySheetDto, Currency>
    {
        public CurrencySheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<CurrencySheetDto> parser,
            ISpreadsheetEntityMapper<CurrencySheetDto, Currency> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter) { }

        protected override DbSet<Currency> DbSet => DbContext.Currencies;

        protected override Entity Entity => Entity.Currency;

        protected override List<Currency> AttachRelatedEntities(List<Currency> entities)
        {
            var items = DbContext.Items.ToList();

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var item = items.SingleOrDefault(s => s.Name.EqualsExact(entity.Item.Name));

                if (item is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching Item {entity.Item.Name}, skipping currency.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.ItemId = item.Id;
                entity.Item = item;
            }

            return entities;
        }
    }
}