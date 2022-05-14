using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Data.Exceptions;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class MoveTutorMoveRepository : HashedEntityRepository<MoveTutorMove>
    {
        public MoveTutorMoveRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override ICollection<MoveTutorMove> PrepareEntitiesForInsertOrUpdate(ICollection<MoveTutorMove> entities)
        {
            var currencies = DbContext.Currencies
                .Include(x => x.Item)
                .AsNoTracking()
                .ToDictionary(x => x.Item.Name, x => x.Id);

            var verifiedEntities = new List<MoveTutorMove>(entities);
            foreach (var entity in entities)
            {
                var canInsertOrUpdate = true;

                DeleteOldPrices(entity);

                canInsertOrUpdate &= TrySetIdForName<MoveTutor>(entity.MoveTutorName, id => entity.MoveTutorId = id);
                canInsertOrUpdate &= TrySetIdForName<Move>(entity.MoveName, id => entity.MoveId = id);

                AddCurrencies(entity, currencies);

                if (!canInsertOrUpdate)
                {
                    verifiedEntities.Remove(entity);
                }
            }

            return base.PrepareEntitiesForInsertOrUpdate(verifiedEntities);
        }

        private void DeleteOldPrices(MoveTutorMove entity)
        {
            DbContext.MoveTutorMovePrices
                .Include(x => x.MoveTutorMove)
                .ThenInclude(x => x.MoveTutor)
                .Include(x => x.MoveTutorMove)
                .ThenInclude(x => x.Move)
                .Where(x =>
                    x.MoveTutorMove.MoveTutor.Name.Equals(entity.MoveTutorName) &&
                    x.MoveTutorMove.Move.Name.Equals(entity.MoveName))
                .DeleteFromQuery();
        }

        private void AddCurrencies(MoveTutorMove entity, Dictionary<string, int> currencies)
        {
            var verifiedPrices = new List<MoveTutorMovePrice>(entity.Price);
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
                    ReportInsertOrUpdateException(typeof(MoveTutorMovePrice), exception);
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