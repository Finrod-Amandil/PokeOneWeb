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

        protected override ICollection<Spawn> PrepareEntitiesForInsertOrUpdate(ICollection<Spawn> entities)
        {
            var seasons = DbContext.Seasons
                .ToDictionary(x => x.Abbreviation, x => x.Id);

            var timesOfDay = DbContext.TimesOfDay
                .ToDictionary(x => x.Abbreviation, x => x.Id);

            var verifiedEntities = new List<Spawn>(entities);
            foreach (var entity in entities)
            {
                var canInsertOrUpdate = true;

                canInsertOrUpdate &= TrySetIdForName<PokemonForm>(
                    entity.PokemonFormName,
                    id => entity.PokemonFormId = id);

                canInsertOrUpdate &= TrySetIdForName<Location>(
                    entity.LocationName,
                    id => entity.LocationId = id);

                canInsertOrUpdate &= TrySetIdForName<SpawnType>(
                    entity.SpawnTypeName,
                    id => entity.SpawnTypeId = id);

                // If any opportunity fails to be attached, skip entire spawn
                foreach (var opportunity in entity.SpawnOpportunities)
                {
                    canInsertOrUpdate &= TryAddSeason(opportunity, seasons);
                    canInsertOrUpdate &= TryAddTimeOfDay(opportunity, timesOfDay);
                }

                if (!canInsertOrUpdate)
                {
                    verifiedEntities.Remove(entity);
                }
            }

            return base.PrepareEntitiesForInsertOrUpdate(verifiedEntities);
        }

        private bool TryAddSeason(SpawnOpportunity opportunity, Dictionary<string, int> seasons)
        {
            var success = seasons.TryGetValue(opportunity.SeasonAbbreviation, out var seasonId);

            if (!success)
            {
                var exception = new RelatedEntityNotFoundException(
                    nameof(SpawnOpportunity),
                    nameof(Season),
                    opportunity.SeasonAbbreviation);
                ReportInsertOrUpdateException(typeof(Spawn), exception);
            }
            else
            {
                opportunity.SeasonId = seasonId;
            }

            return success;
        }

        private bool TryAddTimeOfDay(SpawnOpportunity opportunity, Dictionary<string, int> timesOfDay)
        {
            var success = timesOfDay.TryGetValue(opportunity.TimeOfDayAbbreviation, out var timeOfDayId);

            if (!success)
            {
                var exception = new RelatedEntityNotFoundException(
                    nameof(SpawnOpportunity),
                    nameof(TimeOfDay),
                    opportunity.TimeOfDayAbbreviation);
                ReportInsertOrUpdateException(typeof(Spawn), exception);
            }
            else
            {
                opportunity.TimeOfDayId = timeOfDayId;
            }

            return success;
        }
    }
}