using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MoveTutors
{
    public class MoveTutorSheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<MoveTutorSheetRepository> _logger;
        private readonly ISheetRowParser<MoveTutorDto> _parser;
        private readonly ISpreadsheetEntityMapper<MoveTutorDto, MoveTutor> _mapper;

        public MoveTutorSheetRepository(
            ApplicationDbContext dbContext,
            ILogger<MoveTutorSheetRepository> logger,
            ISheetRowParser<MoveTutorDto> parser,
            ISpreadsheetEntityMapper<MoveTutorDto, MoveTutor> mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.MoveTutors
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.MoveTutors
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.MoveTutors.RemoveRange(entities);
            _dbContext.SaveChanges();

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();

            entities = AttachRelatedEntities(entities);

            _dbContext.MoveTutors.AddRange(entities);
            _dbContext.SaveChanges();

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.MoveTutors
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes).ToList();

            AttachRelatedEntities(updatedEntities);

            _dbContext.SaveChanges();

            return updatedEntities.Count;
        }

        private Dictionary<RowHash, MoveTutorDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }

        private List<MoveTutor> AttachRelatedEntities(List<MoveTutor> entities)
        {
            var locations = _dbContext.Locations.ToList();

            if (!locations.Any())
            {
                throw new Exception("Tried to import move tutors, but no" +
                                    "Locations were present in the database.");
            }

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var location = locations.SingleOrDefault(l => l.Name.EqualsExact(entity.Location.Name));

                if (location is null)
                {
                    _logger.LogWarning($"Could not find matching Location {entity.Location.Name}, skipping move tutor.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.LocationId = location.Id;
                entity.Location = location;
            }

            return entities;
        }
    }
}
