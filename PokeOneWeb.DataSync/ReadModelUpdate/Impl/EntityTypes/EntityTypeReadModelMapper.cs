using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.Data.ReadModels.Enums;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;

namespace PokeOneWeb.DataSync.ReadModelUpdate.Impl.EntityTypes
{
    public class EntityTypeReadModelMapper : IReadModelMapper<EntityTypeReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public EntityTypeReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDictionary<EntityTypeReadModel, DbAction> MapFromDatabase(SpreadsheetImportReport importReport)
        {
            var pokemonMappings = _dbContext.PokemonVarieties
                .Where(p => p.DoInclude)
                .AsNoTracking()
                .Select(p => new EntityTypeReadModel
                {
                    ResourceName = p.ResourceName,
                    EntityType = EntityType.PokemonVariety
                });

            var locationMappings = _dbContext.LocationGroups
                .AsNoTracking()
                .Select(l => new EntityTypeReadModel
                {
                    ResourceName = l.ResourceName,
                    EntityType = EntityType.Location
                });

            var itemMappings = _dbContext.Items
                .Where(i => i.DoInclude)
                .AsNoTracking()
                .Select(i => new EntityTypeReadModel
                {
                    ResourceName = i.ResourceName,
                    EntityType = EntityType.Item
                });

            var entityTypeMappings = pokemonMappings.ToList();
            entityTypeMappings.AddRange(locationMappings);
            entityTypeMappings.AddRange(itemMappings);

            return entityTypeMappings.ToDictionary(x => x, _ => DbAction.Create);
        }
    }
}