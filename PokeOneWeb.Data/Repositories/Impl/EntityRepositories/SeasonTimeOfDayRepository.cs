using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class SeasonTimeOfDayRepository : HashedEntityRepository<SeasonTimeOfDay>
    {
        public SeasonTimeOfDayRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override void PrepareEntitiesForInsertOrUpdate(SeasonTimeOfDay entity)
        {
            entity.SeasonId = GetRequiredIdForName<Season>(entity.SeasonName);
            entity.TimeOfDayId = GetRequiredIdForName<TimeOfDay>(entity.TimeOfDayName);
        }
    }
}