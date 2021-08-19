using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.SpawnTypes
{
    public class SpawnTypeSheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ISheetRowParser<SpawnTypeSheetDto> _parser;
        private readonly ISpreadsheetEntityMapper<SpawnTypeSheetDto, SpawnType> _mapper;

        public SpawnTypeSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<SpawnTypeSheetDto> parser,
            ISpreadsheetEntityMapper<SpawnTypeSheetDto, SpawnType> mapper)
        {
            _dbContext = dbContext;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.SpawnTypes
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.SpawnTypes
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.SpawnTypes.RemoveRange(entities);
            _dbContext.SaveChanges();

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();

            _dbContext.SpawnTypes.AddRange(entities);
            _dbContext.SaveChanges();

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.SpawnTypes
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes);
            _dbContext.SaveChanges();

            return updatedEntities.Count();
        }

        private Dictionary<RowHash, SpawnTypeSheetDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }
    }
}
