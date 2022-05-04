using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class LocationRepository : HashedEntityRepository<Location>
    {
        public LocationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override void Insert(ICollection<Location> entities)
        {
            base.Insert(entities);
            DeleteUnusedParentEntities();
        }

        public override void Update(ICollection<Location> entities)
        {
            base.Update(entities);
            DeleteUnusedParentEntities();
        }

        public override void DeleteByIdHashes(ICollection<string> idHashes)
        {
            base.DeleteByIdHashes(idHashes);
            DeleteUnusedParentEntities();
        }

        protected override ICollection<Location> PrepareEntitiesForInsertOrUpdate(ICollection<Location> entities)
        {
            AddOrUpdateLocationGroups(entities);

            var verifiedEntities = new List<Location>(entities);
            foreach (var entity in entities)
            {
                var canInsertOrUpdate = true;

                canInsertOrUpdate &= TrySetIdForName<LocationGroup>(
                    entity.LocationGroup.Name,
                    id => entity.LocationGroupId = id);

                if (!canInsertOrUpdate)
                {
                    verifiedEntities.Remove(entity);
                }

                entity.LocationGroup = null;
            }

            return base.PrepareEntitiesForInsertOrUpdate(verifiedEntities);
        }

        private void DeleteUnusedParentEntities()
        {
            DbContext.LocationGroups
                .Where(x => x.Locations.Count == 0)
                .DeleteFromQuery();
        }

        private void AddOrUpdateLocationGroups(ICollection<Location> entities)
        {
            var distinctLocationGroups = entities
                .Select(x => x.LocationGroup)
                .DistinctBy(x => x.Name)
                .ToList();

            var verifiedLocationGroups = new List<LocationGroup>(distinctLocationGroups);
            foreach (var locationGroup in distinctLocationGroups)
            {
                var canInsertOrUpdate = true;

                canInsertOrUpdate &= TrySetIdForName<Region>(
                    locationGroup.Name,
                    id => locationGroup.RegionId = id);

                if (!canInsertOrUpdate)
                {
                    verifiedLocationGroups.Remove(locationGroup);
                }
            }

            AddOrUpdateRelatedEntitiesByName(verifiedLocationGroups);
        }
    }
}