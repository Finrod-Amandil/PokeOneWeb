using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Currencies
{
    public class CurrencySheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CurrencySheetRepository> _logger;
        private readonly ISheetRowParser<CurrencyDto> _parser;
        private readonly ISpreadsheetEntityMapper<CurrencyDto, Currency> _mapper;

        public CurrencySheetRepository(
            ApplicationDbContext dbContext,
            ILogger<CurrencySheetRepository> logger,
            ISheetRowParser<CurrencyDto> parser,
            ISpreadsheetEntityMapper<CurrencyDto, Currency> mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.Currencies
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.Currencies
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.Currencies.RemoveRange(entities);
            _dbContext.SaveChanges();

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();

            entities = AttachRelatedEntities(entities);

            _dbContext.Currencies.AddRange(entities);
            _dbContext.SaveChanges();

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.Currencies
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes).ToList();

            AttachRelatedEntities(updatedEntities);

            _dbContext.SaveChanges();

            return updatedEntities.Count;
        }

        private Dictionary<RowHash, CurrencyDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }

        private List<Currency> AttachRelatedEntities(List<Currency> entities)
        {
            var items = _dbContext.Items.ToList();

            if (!items.Any())
            {
                throw new Exception("Tried to import currencies, but no" +
                                    "Items were present in the database.");
            }

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var item = items.SingleOrDefault(s => s.Name.EqualsExact(entity.Item.Name));

                if (item is null)
                {
                    _logger.LogWarning($"Could not find matching Item {entity.Item.Name}, skipping currency.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.ItemId = item.Id;
                entity.Item = item;
            }

            return entities;
        }
    }
}
