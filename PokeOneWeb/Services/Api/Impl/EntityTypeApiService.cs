using System.Linq;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels.Enums;
using PokeOneWeb.Dtos;
using PokeOneWeb.WebApi.Dtos;

namespace PokeOneWeb.Services.Api.Impl
{
    public class EntityTypeApiService : IEntityTypeApiService
    {
        private readonly ReadModelDbContext _dbContext;

        public EntityTypeApiService(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public EntityTypeDto GetEntityTypeForPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return new EntityTypeDto {EntityType = EntityType.Unknown};
            }

            var matchingMapping = _dbContext.EntityTypeReadModels
                .SingleOrDefault(e => e.ResourceName.Equals(path));

            return matchingMapping is null ? 
                new EntityTypeDto { EntityType = EntityType.Unknown } : 
                new EntityTypeDto { EntityType = matchingMapping.EntityType };
        }
    }
}
