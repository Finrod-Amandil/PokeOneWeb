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

        public override void Insert(ICollection<MoveTutorMove> entities)
        {
            var currencies = DbContext.Currencies
                .Include(x => x.Item)
                .ToDictionary(x => x.Item.Name, x => x.Id);

            foreach (var entity in entities)
            {
                // Delete old prices
                DbContext.MoveTutorMovePrices
                    .Include(x => x.MoveTutorMove)
                    .ThenInclude(x => x.MoveTutor)
                    .Include(x => x.MoveTutorMove)
                    .ThenInclude(x => x.Move)
                    .Where(x =>
                        x.MoveTutorMove.MoveTutor.Name.Equals(entity.MoveTutorName) &&
                        x.MoveTutorMove.Move.Name.Equals(entity.MoveName))
                    .DeleteFromQuery();

                entity.MoveTutorId = GetRequiredIdForName<MoveTutor>(entity.MoveTutorName);
                entity.MoveId = GetRequiredIdForName<Move>(entity.MoveName);

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