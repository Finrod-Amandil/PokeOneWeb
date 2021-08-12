using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.ElementalTypeRelations
{
    public class ElementalTypeRelationSheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly ISheetRowParser<ElementalTypeRelationDto> _parser;
        private readonly ISpreadsheetEntityMapper<ElementalTypeRelationDto, ElementalTypeRelation> _mapper;

        public ElementalTypeRelationSheetRepository(
            ApplicationDbContext dbContext,
            ILogger<ElementalTypeRelationSheetRepository> logger,
            ISheetRowParser<ElementalTypeRelationDto> parser,
            ISpreadsheetEntityMapper<ElementalTypeRelationDto, ElementalTypeRelation> mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.ElementalTypeRelations
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.ElementalTypeRelations
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.ElementalTypeRelations.RemoveRange(entities);
            _dbContext.SaveChanges();

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();

            entities = AttachRelatedEntities(entities);

            _dbContext.ElementalTypeRelations.AddRange(entities);
            _dbContext.SaveChanges();

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.ElementalTypeRelations
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes).ToList();

            AttachRelatedEntities(updatedEntities);

            _dbContext.SaveChanges();

            return updatedEntities.Count;
        }

        private Dictionary<RowHash, ElementalTypeRelationDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }

        private List<ElementalTypeRelation> AttachRelatedEntities(List<ElementalTypeRelation> entities)
        {
            var types = _dbContext.ElementalTypes.ToList();

            if (!types.Any())
            {
                throw new Exception("Tried to import elemental type relations, but no" +
                                    "elemental types were present in the database.");
            }

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var attackingType = types.SingleOrDefault(t =>
                    t.Name.Equals(entity.AttackingType.Name, StringComparison.Ordinal));

                if (attackingType is null)
                {
                    _logger.LogWarning($"No unique matching type could be found for attacking type" +
                                       $" {entity.AttackingType.Name}. Skipping.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                var defendingType = types.SingleOrDefault(t =>
                    t.Name.Equals(entity.DefendingType.Name, StringComparison.Ordinal));

                if (defendingType is null)
                {
                    _logger.LogWarning($"No unique matching type could be found for defending type" +
                                       $" {entity.DefendingType.Name}. Skipping.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.AttackingTypeId = attackingType.Id;
                entity.AttackingType = attackingType;
                entity.DefendingTypeId = defendingType.Id;
                entity.DefendingType = defendingType;

                _dbContext.ElementalTypeRelations.Add(entity);
            }

            return entities;
        }
    }
}
