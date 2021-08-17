using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.SeasonTimesOfDay
{
    public class SeasonTimeOfDaySheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<SeasonTimeOfDaySheetRepository> _logger;
        private readonly ISheetRowParser<SeasonTimeOfDaySheetDto> _parser;
        private readonly ISpreadsheetEntityMapper<SeasonTimeOfDaySheetDto, SeasonTimeOfDay> _mapper;

        public SeasonTimeOfDaySheetRepository(
            ApplicationDbContext dbContext,
            ILogger<SeasonTimeOfDaySheetRepository> logger,
            ISheetRowParser<SeasonTimeOfDaySheetDto> parser,
            ISpreadsheetEntityMapper<SeasonTimeOfDaySheetDto, SeasonTimeOfDay> mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.SeasonTimesOfDay
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.SeasonTimesOfDay
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.SeasonTimesOfDay.RemoveRange(entities);
            _dbContext.SaveChanges();

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();

            entities = AttachRelatedEntities(entities);

            _dbContext.SeasonTimesOfDay.AddRange(entities);
            _dbContext.SaveChanges();

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.SeasonTimesOfDay
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes).ToList();

            AttachRelatedEntities(updatedEntities);

            _dbContext.SaveChanges();

            return updatedEntities.Count;
        }

        private Dictionary<RowHash, SeasonTimeOfDaySheetDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }

        private List<SeasonTimeOfDay> AttachRelatedEntities(List<SeasonTimeOfDay> entities)
        {
            var seasons = _dbContext.Seasons.ToList();
            var timesOfDay = _dbContext.TimesOfDay.ToList();

            if (!seasons.Any())
            {
                throw new Exception("Tried to import season times of day, but no" +
                                    "Seasons were present in the database.");
            }

            if (!timesOfDay.Any())
            {
                throw new Exception("Tried to import season times of day, but no" +
                                    "Times of Day were present in the database.");
            }

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var season = seasons.SingleOrDefault(s => s.Name.EqualsExact(entity.Season.Name));

                if (season is null)
                {
                    _logger.LogWarning($"Could not find matching Season {entity.Season.Name}, skipping season time of day.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.SeasonId = season.Id;
                entity.Season = season;

                var timeOfDay = timesOfDay.SingleOrDefault(t => t.Name.EqualsExact(entity.TimeOfDay.Name));

                if (timeOfDay is null)
                {
                    _logger.LogWarning($"Could not find matching TimeOfDay {entity.TimeOfDay.Name}, skipping season time of day.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.TimeOfDayId = timeOfDay.Id;
                entity.TimeOfDay = timeOfDay;
            }

            return entities;
        }
    }
}
