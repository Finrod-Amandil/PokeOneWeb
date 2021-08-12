using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Regions
{
    public class RegionSheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<RegionSheetRepository> _logger;
        private readonly ISheetRowParser<RegionDto> _parser;
        private readonly ISpreadsheetEntityMapper<RegionDto, Region> _mapper;

        public RegionSheetRepository(
            ApplicationDbContext dbContext,
            ILogger<RegionSheetRepository> logger,
            ISheetRowParser<RegionDto> parser,
            ISpreadsheetEntityMapper<RegionDto, Region> mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.Regions
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.Regions
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.Regions.RemoveRange(entities);
            _dbContext.SaveChanges();

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();

            entities = AttachRelatedEntities(entities);

            _dbContext.Regions.AddRange(entities);
            _dbContext.SaveChanges();

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.Regions
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes).ToList();

            AttachRelatedEntities(updatedEntities);

            _dbContext.SaveChanges();

            return updatedEntities.Count;
        }

        private Dictionary<RowHash, RegionDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }

        private List<Region> AttachRelatedEntities(List<Region> entities)
        {
            var events = _dbContext.Events.ToList();

            foreach (var entity in entities)
            {
                if (entity.Event == null) continue;
                var eventEntity = events.SingleOrDefault(s => s.Name.EqualsExact(entity.Event.Name));

                if (eventEntity is null)
                {
                    _logger.LogWarning($"Could not find matching Event {entity.Event.Name}, setting null.");
                    continue;
                }

                entity.EventId = eventEntity.Id;
                entity.Event = eventEntity;
            }

            return entities;
        }
    }
}
