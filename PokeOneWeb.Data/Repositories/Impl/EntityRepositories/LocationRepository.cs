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

        protected override void AddIdsForNames(Location entity)
        {
            entity.LocationGroupId = GetRequiredIdForName<LocationGroup>(entity.LocationGroupName);
        }

        public override void Insert(ICollection<Location> entities)
        {
            PrepareEntitiesForInsertOrUpdate(entities);
            base.Insert(entities);
            DbContext.LocationGroups.Where(x => x.Locations.Count == 0).DeleteFromQuery();
        }

        public override void Update(ICollection<Location> entities)
        {
            PrepareEntitiesForInsertOrUpdate(entities);
            base.Update(entities);
            DbContext.LocationGroups.Where(x => x.Locations.Count == 0).DeleteFromQuery();
        }

        public override void DeleteByIdHashes(ICollection<string> idHashes)
        {
            base.DeleteByIdHashes(idHashes);
            DbContext.LocationGroups.Where(x => x.Locations.Count == 0).DeleteFromQuery();
        }

        private void PrepareEntitiesForInsertOrUpdate(ICollection<Location> entities)
        {
            var entitiesAsList = entities.ToList();

            // Lookup ids of related entities
            entitiesAsList.ForEach(x =>
                x.LocationGroup.RegionId = GetRequiredIdForName<Region>(x.LocationGroup.RegionName));

            // Add/Insert Location Groups
            AddOrUpdateRelatedEntitiesByName(entities.Select(x => x.LocationGroup));

            // Detach Location Group so that it does not get inserted again
            entitiesAsList.ForEach(x => x.LocationGroupName = x.LocationGroup.Name);
            entitiesAsList.ForEach(x => x.LocationGroup = null);
        }
    }
}