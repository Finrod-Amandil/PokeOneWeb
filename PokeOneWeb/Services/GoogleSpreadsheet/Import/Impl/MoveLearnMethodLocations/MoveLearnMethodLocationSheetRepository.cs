using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data.Entities;
using Z.EntityFramework.Plus;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MoveLearnMethodLocations
{
    public class MoveLearnMethodLocationSheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<MoveLearnMethodLocationSheetRepository> _logger;
        private readonly ISheetRowParser<MoveLearnMethodLocationSheetDto> _parser;
        private readonly ISpreadsheetEntityMapper<MoveLearnMethodLocationSheetDto, MoveLearnMethodLocation> _mapper;

        public MoveLearnMethodLocationSheetRepository(
            ApplicationDbContext dbContext,
            ILogger<MoveLearnMethodLocationSheetRepository> logger,
            ISheetRowParser<MoveLearnMethodLocationSheetDto> parser,
            ISpreadsheetEntityMapper<MoveLearnMethodLocationSheetDto, MoveLearnMethodLocation> mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.MoveLearnMethodLocations
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.MoveLearnMethodLocations
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.MoveLearnMethodLocations.RemoveRange(entities);
            _dbContext.SaveChanges();

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();

            entities = AttachRelatedEntities(entities);

            _dbContext.MoveLearnMethodLocations.AddRange(entities);
            _dbContext.SaveChanges();

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.MoveLearnMethodLocations
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes).ToList();

            AttachRelatedEntities(updatedEntities);

            _dbContext.SaveChanges();

            return updatedEntities.Count;
        }

        private Dictionary<RowHash, MoveLearnMethodLocationSheetDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }

        private List<MoveLearnMethodLocation> AttachRelatedEntities(List<MoveLearnMethodLocation> entities)
        {
            var moveLearnMethods = _dbContext.MoveLearnMethods.ToList();
            var locations = _dbContext.Locations.ToList();
            var currencies = _dbContext.Currencies
                .IncludeOptimized(c => c.Item)
                .ToList();

            if (!locations.Any())
            {
                throw new Exception("Tried to import move learn method locations, but no" +
                                    "locations were present in the database.");
            }

            if (!currencies.Any())
            {
                throw new Exception("Tried to import move learn method locations, but no" +
                                    "currencies were present in the database.");
            }

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var moveLearnMethod = moveLearnMethods
                    .SingleOrDefault(m => m.Name.EqualsExact(entity.MoveLearnMethod.Name));
                if (moveLearnMethod is null)
                {
                    moveLearnMethod = entity.MoveLearnMethod;
                    moveLearnMethods.Add(moveLearnMethod);
                }

                entity.MoveLearnMethodId = moveLearnMethod.Id;
                entity.MoveLearnMethod = moveLearnMethod;

                var location = locations.SingleOrDefault(l => l.Name.EqualsExact(entity.Location.Name));
                if (location is null)
                {
                    _logger.LogWarning($"Could not find matching Location {entity.Location.Name}, skipping move learn method location.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.LocationId = location.Id;
                entity.Location = location;

                for (var j = 0; j < entity.Price.Count; j++)
                {
                    var price = entity.Price[j];

                    var currency = currencies.SingleOrDefault(c =>
                        c.Item.Name.EqualsExact(price.CurrencyAmount.Currency.Item.Name));

                    if (currency is null)
                    {
                        _logger.LogWarning("Could not find matching Currency " +
                                           $"{price.CurrencyAmount.Currency.Item.Name}, " +
                                           "skipping move learn method location price.");
                        entity.Price.Remove(price);
                        j--;
                        continue;
                    }

                    price.CurrencyAmount.CurrencyId = currency.Id;
                    price.CurrencyAmount.Currency = currency;
                }
            }

            return entities;
        }
    }
}
