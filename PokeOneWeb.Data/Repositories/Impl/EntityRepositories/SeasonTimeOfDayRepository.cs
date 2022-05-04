using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class SeasonTimeOfDayRepository : HashedEntityRepository<SeasonTimeOfDay>
    {
        public SeasonTimeOfDayRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override List<Func<SeasonTimeOfDay, bool>> PreparationSteps => new()
        {
            entity => TrySetIdForName<Season>(entity.SeasonName, id => entity.SeasonId = id),
            entity => TrySetIdForName<TimeOfDay>(entity.TimeOfDayName, id => entity.TimeOfDayId = id),
        };
    }
}