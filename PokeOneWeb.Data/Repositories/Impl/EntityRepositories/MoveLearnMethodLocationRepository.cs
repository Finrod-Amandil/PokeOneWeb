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

        public override void Insert(ICollection<MoveLearnMethodLocation> entities)
        {
            AddOrUpdateRelatedEntitiesByName(entities.Select(x => x.MoveLearnMethod));

            var currencies = DbContext.Currencies
                .Include(x => x.Item)
                .ToDictionary(x => x.Item.Name, x => x.Id);

            foreach (var entity in entities)
            {
                // Delete old prices
                DbContext.MoveLearnMethodLocationPrices
                    .Include(x => x.MoveLearnMethodLocation)
                    .ThenInclude(x => x.MoveLearnMethod)
                    .Include(x => x.MoveLearnMethodLocation)
                    .ThenInclude(x => x.Location)
                    .Where(x =>
                        x.MoveLearnMethodLocation.MoveLearnMethod.Name.Equals(entity.MoveLearnMethod.Name) &&
                        x.MoveLearnMethodLocation.Location.Name.Equals(entity.LocationName))
                    .DeleteFromQuery();

                entity.LocationId = GetRequiredIdForName<Location>(entity.LocationName);
                entity.MoveLearnMethodId = GetRequiredIdForName<MoveLearnMethod>(entity.MoveLearnMethod.Name);
                entity.MoveLearnMethod = null;

                // Attach currencies
                foreach (var ca in entity.Price.Select(p => p.CurrencyAmount))
                {
                    ca.CurrencyId = currencies.TryGetValue(ca.CurrencyName, out var id)
                        ? id
                        : throw new RelatedEntityNotFoundException(
                            nameof(CurrencyAmount),
                            nameof(Currency),
                            ca.CurrencyName);
                }
            }

            base.Insert(entities);
        }
    }
}