using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Moves
{
    public class MoveSheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<MoveSheetRepository> _logger;
        private readonly ISheetRowParser<MoveDto> _parser;
        private readonly ISpreadsheetEntityMapper<MoveDto, Move> _mapper;

        public MoveSheetRepository(
            ApplicationDbContext dbContext,
            ILogger<MoveSheetRepository> logger,
            ISheetRowParser<MoveDto> parser,
            ISpreadsheetEntityMapper<MoveDto, Move> mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.Moves
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.Moves
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.Moves.RemoveRange(entities);
            _dbContext.SaveChanges();

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();

            entities = AttachRelatedEntities(entities);

            _dbContext.Moves.AddRange(entities);
            _dbContext.SaveChanges();

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.Moves
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes).ToList();

            AttachRelatedEntities(updatedEntities);

            _dbContext.SaveChanges();

            return updatedEntities.Count;
        }

        private Dictionary<RowHash, MoveDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }

        private List<Move> AttachRelatedEntities(List<Move> entities)
        {
            var damageClasses = _dbContext.MoveDamageClasses.ToList();
            var types = _dbContext.ElementalTypes.ToList();

            if (!damageClasses.Any())
            {
                throw new Exception("Tried to import moves, but no" +
                                    "Move damage classes were present in the database.");
            }

            if (!types.Any())
            {
                throw new Exception("Tried to import moves, but no" +
                                    "elemental types were present in the database.");
            }

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var damageClass = damageClasses.SingleOrDefault(c => c.Name.EqualsExact(entity.DamageClass.Name));

                if (damageClass is null)
                {
                    _logger.LogWarning($"Could not find matching MoveDamageClass {entity.DamageClass.Name}, skipping move.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.DamageClassId = damageClass.Id;
                entity.DamageClass = damageClass;

                var type = types.SingleOrDefault(t => t.Name.EqualsExact(entity.ElementalType.Name));

                if (type is null)
                {
                    _logger.LogWarning($"Could not find matching ElementalType {entity.ElementalType.Name}, skipping move.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.ElementalTypeId = type.Id;
                entity.ElementalType = type;
            }

            return entities;
        }
    }
}
