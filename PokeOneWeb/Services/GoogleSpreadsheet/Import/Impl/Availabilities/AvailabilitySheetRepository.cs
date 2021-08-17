using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Availabilities
{
    public class AvailabilitySheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ISheetRowParser<AvailabilitySheetDto> _parser;
        private readonly ISpreadsheetEntityMapper<AvailabilitySheetDto, PokemonAvailability> _mapper;

        public AvailabilitySheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<AvailabilitySheetDto> parser,
            ISpreadsheetEntityMapper<AvailabilitySheetDto, PokemonAvailability> mapper)
        {
            _dbContext = dbContext;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.PokemonAvailabilities
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.PokemonAvailabilities
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.PokemonAvailabilities.RemoveRange(entities);
            _dbContext.SaveChanges();

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();
            _dbContext.PokemonAvailabilities.AddRange(entities);
            _dbContext.SaveChanges();

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.PokemonAvailabilities
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes);
            _dbContext.SaveChanges();

            return updatedEntities.Count();
        }

        private Dictionary<RowHash, AvailabilitySheetDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }
    }
}
