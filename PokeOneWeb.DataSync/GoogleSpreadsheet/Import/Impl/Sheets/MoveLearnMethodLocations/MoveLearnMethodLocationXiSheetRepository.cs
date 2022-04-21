using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;
using Z.EntityFramework.Plus;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveLearnMethodLocations
{
    public class MoveLearnMethodLocationXiSheetRepository
        : XSheetRepository<MoveLearnMethodLocationSheetDto, MoveLearnMethodLocation>
    {
        public MoveLearnMethodLocationXiSheetRepository(
            ApplicationDbContext dbContext,
            XISheetRowParser<MoveLearnMethodLocationSheetDto> parser,
            XISpreadsheetEntityMapper<MoveLearnMethodLocationSheetDto, MoveLearnMethodLocation> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter)
        {
        }

        protected override DbSet<MoveLearnMethodLocation> DbSet => DbContext.MoveLearnMethodLocations;

        protected override Entity Entity => Entity.MoveLearnMethodLocation;

        protected override List<MoveLearnMethodLocation> AttachRelatedEntities(
            List<MoveLearnMethodLocation> entities)
        {
            var moveLearnMethods = DbContext.MoveLearnMethods.ToList();
            var locations = DbContext.Locations.ToList();
            var currencies = DbContext.Currencies
                .IncludeOptimized(c => c.Item)
                .ToList();

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var moveLearnMethod = moveLearnMethods
                    .SingleOrDefault(m => m.Name.EqualsExact(entity.MoveLearnMethod.Name));
                if (moveLearnMethod is null)
                {
                    moveLearnMethod = entity.MoveLearnMethod;
                    moveLearnMethods.Add(moveLearnMethod);
                }

                entity.MoveLearnMethodId = moveLearnMethod.Id;
                entity.MoveLearnMethod = moveLearnMethod;

                var location = locations.SingleOrDefault(l => l.Name.EqualsExact(entity.Location.Name));
                if (location is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching Location {entity.Location.Name}, skipping move learn method location.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.LocationId = location.Id;
                entity.Location = location;

                for (var j = 0; j < entity.Price.Count; j++)
                {
                    var price = entity.Price[j];

                    var currency = currencies.SingleOrDefault(c =>
                        c.Item.Name.EqualsExact(price.CurrencyAmount.Currency.Item.Name));

                    if (currency is null)
                    {
                        Reporter.ReportError(Entity, entity.IdHash,
                            "Could not find matching Currency " +
                            $"{price.CurrencyAmount.Currency.Item.Name}, " +
                            "skipping move learn method location price.");

                        entity.Price.Remove(price);
                        j--;
                        continue;
                    }

                    price.CurrencyAmount.CurrencyId = currency.Id;
                    price.CurrencyAmount.Currency = currency;
                }
            }

            return entities;
        }
    }
}