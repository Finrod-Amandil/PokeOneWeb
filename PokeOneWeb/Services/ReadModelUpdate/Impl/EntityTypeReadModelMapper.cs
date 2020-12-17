using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;

namespace PokeOneWeb.Services.ReadModelUpdate.Impl
{
    public class EntityTypeReadModelMapper : IReadModelMapper<EntityTypeReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public EntityTypeReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<EntityTypeReadModel> MapFromDatabase()
        {
            var pokemonMappings = _dbContext.PokemonVarieties
                .Where(p => p.DoInclude)
                .Select(p => new EntityTypeReadModel
                {
                    ResourceName = p.ResourceName,
                    EntityType = EntityType.PokemonVariety
                });

            var locationMappings = _dbContext.LocationGroups
                .Select(l => new EntityTypeReadModel {
                    ResourceName = l.Name.Replace(' ', '-').ToLower(),
                    EntityType = EntityType.Location
                });

            var itemMappings = _dbContext.Items
                .Select(i => new EntityTypeReadModel
                {
                    ResourceName = i.Name.Replace(' ', '-').ToLower(),
                    EntityType = EntityType.Item
                });

            var entityTypeMappings = pokemonMappings.ToList();
            entityTypeMappings.AddRange(locationMappings);
            entityTypeMappings.AddRange(itemMappings);

            return entityTypeMappings;
        }
    }
}
