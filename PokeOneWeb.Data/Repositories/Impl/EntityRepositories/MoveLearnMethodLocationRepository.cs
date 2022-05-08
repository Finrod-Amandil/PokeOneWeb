using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Data.Exceptions;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class MoveLearnMethodLocationRepository : HashedEntityRepository<MoveLearnMethodLocation>
    {
        public MoveLearnMethodLocationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override ICollection<MoveLearnMethodLocation> PrepareEntitiesForInsertOrUpdate(ICollection<MoveLearnMethodLocation> entities)
        {
            AddOrUpdateRelatedEntitiesByName(entities.Select(x => x.MoveLearnMethod));

            var currencies = DbContext.Currencies
                .Include(x => x.Item)
                .AsNoTracking()
                .ToDictionary(x => x.Item.Name, x => x.Id);

            var verifiedEntities = new List<MoveLearnMethodLocation>(entities);
            foreach (var entity in entities)
            {
                var canInsertOrUpdate = true;

                DeleteOldPrices(entity);

                canInsertOrUpdate &= TrySetIdForName<Location>(entity.LocationName, id => entity.LocationId = id);
                canInsertOrUpdate &= TrySetIdForName<MoveLearnMethod>(entity.MoveLearnMethod.Name, id => entity.MoveLearnMethodId = id);
                entity.MoveLearnMethod = null;

                AddCurrencies(entity, currencies);

                if (!canInsertOrUpdate)
                {
                    verifiedEntities.Remove(entity);
                }
            }

            return base.PrepareEntitiesForInsertOrUpdate(verifiedEntities);
        }

        private void DeleteOldPrices(MoveLearnMethodLocation entity)
        {
            DbContext.MoveLearnMethodLocationPrices
                .Include(x => x.MoveLearnMethodLocation)
                .ThenInclude(x => x.MoveLearnMethod)
                .Include(x => x.MoveLearnMethodLocation)
                .ThenInclude(x => x.Location)
                .Where(x =>
                    x.MoveLearnMethodLocation.MoveLearnMethod.Name.Equals(entity.MoveLearnMethod.Name) &&
                    x.MoveLearnMethodLocation.Location.Name.Equals(entity.LocationName))
                .DeleteFromQuery();
        }

        private void AddCurrencies(MoveLearnMethodLocation entity, Dictionary<string, int> currencies)
        {
            var verifiedPrices = new List<MoveLearnMethodLocationPrice>(entity.Price);
            foreach (var price in entity.Price)
            {
                var canInsertOrUpdate = true;

                var ca = price.CurrencyAmount;
                canInsertOrUpdate &= currencies.TryGetValue(ca.CurrencyName, out var id);

                if (!canInsertOrUpdate)
                {
                    var exception = new RelatedEntityNotFoundException(
                        nameof(CurrencyAmount),
                        nameof(Currency),
                        ca.CurrencyName);
                    ReportInsertOrUpdateException(typeof(MoveLearnMethodLocation), exception);
                    verifiedPrices.Remove(price);
                }
                else
                {
                    ca.CurrencyId = id;
                }
            }

            entity.Price = verifiedPrices;
        }
    }
}