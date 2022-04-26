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
            var entitiesAsList = entities.ToList();

            // Add/Insert Location Groups
            AddOrUpdateRelatedEntitiesByName(entities.Select(x => x.LocationGroup));

            // Detach Location Group so that it does not get inserted again
            entitiesAsList.ForEach(x => x.LocationGroupName = x.LocationGroup.Name);
            entitiesAsList.ForEach(x => x.LocationGroup = null);

            // Insert Locations
            base.Insert(entities);

            // Delete unused LocationGroups
            DeleteUnusedParentalRelatedEntities<LocationGroup, Location>(x => x.Locations);
        }

        public override void Update(ICollection<Location> entities)
        {
            var entitiesAsList = entities.ToList();

            // Add/Insert Location Groups
            AddOrUpdateRelatedEntitiesByName(entities.Select(x => x.LocationGroup));

            // Detach Location Group so that it does not get inserted again
            entitiesAsList.ForEach(x => x.LocationGroupName = x.LocationGroup.Name);
            entitiesAsList.ForEach(x => x.LocationGroup = null);

            // Update locations
            base.Update(entities);

            // Delete unused LocationGroups
            DeleteUnusedParentalRelatedEntities<LocationGroup, Location>(x => x.Locations);
        }

        public override void DeleteByIdHashes(ICollection<string> idHashes)
        {
            base.DeleteByIdHashes(idHashes);
            DeleteUnusedParentalRelatedEntities<LocationGroup, Location>(x => x.Locations);
        }
    }
}