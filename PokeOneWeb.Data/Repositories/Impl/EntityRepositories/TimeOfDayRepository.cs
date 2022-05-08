using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class TimeOfDayRepository : HashedEntityRepository<TimeOfDay>
    {
        public TimeOfDayRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}