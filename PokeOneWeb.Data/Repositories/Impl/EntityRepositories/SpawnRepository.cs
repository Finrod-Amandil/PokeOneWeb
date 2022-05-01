using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Data.Exceptions;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class SpawnRepository : HashedEntityRepository<Spawn>
    {
        public SpawnRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override void Insert(ICollection<Spawn> entities)
        {
            var seasons = DbContext.Seasons
                .ToDictionary(x => x.Abbreviation, x => x.Id);

            var timesOfDay = DbContext.TimesOfDay
                .ToDictionary(x => x.Abbreviation, x => x.Id);

            foreach (var entity in entities)
            {
                entity.PokemonFormId = GetRequiredIdForName<PokemonForm>(entity.PokemonFormName);
                entity.LocationId = GetRequiredIdForName<Location>(entity.LocationName);
                entity.SpawnTypeId = GetRequiredIdForName<SpawnType>(entity.SpawnTypeName);

                foreach (var opportunity in entity.SpawnOpportunities)
                {
                    opportunity.SeasonId = seasons.TryGetValue(opportunity.SeasonAbbreviation, out var seasonId)
                        ? seasonId
                        : throw new RelatedEntityNotFoundException(
                            nameof(SpawnOpportunity),
                            nameof(Season),
                            opportunity.SeasonAbbreviation);

                    opportunity.TimeOfDayId = timesOfDay.TryGetValue(opportunity.TimeOfDayAbbreviation, out var timeOfDayId)
                        ? timeOfDayId
                        : throw new RelatedEntityNotFoundException(
                            nameof(SpawnOpportunity),
                            nameof(TimeOfDay),
                            opportunity.TimeOfDayAbbreviation);
                }
            }

            base.Insert(entities);
        }
    }
}