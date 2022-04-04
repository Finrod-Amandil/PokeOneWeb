using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Data.Entities.Interfaces;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl
{
    public abstract class SheetRepository<TDto, TEntity>
        : ISheetRepository
        where TEntity : class, IHashedEntity
        where TDto : ISpreadsheetEntityDto
    {
        protected readonly ApplicationDbContext DbContext;
        protected readonly ISpreadsheetImportReporter Reporter;
        private readonly ISheetRowParser<TDto> _parser;
        private readonly ISpreadsheetEntityMapper<TDto, TEntity> _mapper;

        protected SheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<TDto> parser,
            ISpreadsheetEntityMapper<TDto, TEntity> mapper,
            ISpreadsheetImportReporter reporter)
        {
            DbContext = dbContext;
            Reporter = reporter;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = DbSet
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = DbSet.Where(x => idHashes.Contains(x.IdHash));

            DbSet.RemoveRange(entities);
            DbContext.SaveChanges();

            entities.ToList().ForEach(e => Reporter.ReportDeleted(Entity, e.IdHash, e.Id));

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();
            entities = AttachRelatedEntities(entities);

            DbSet.AddRange(entities);
            DbContext.SaveChanges();

            DeleteOrphans();

            entities.ForEach(e => Reporter.ReportAdded(Entity, e.IdHash, e.Id));

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = DbSet
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes).ToList();
            updatedEntities = AttachRelatedEntities(updatedEntities);

            DbContext.SaveChanges();

            entities.ForEach(e => Reporter.ReportUpdated(Entity, e.IdHash, e.Id));

            return updatedEntities.Count();
        }

        private Dictionary<RowHash, TDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            var result = new Dictionary<RowHash, TDto>();

            foreach (var (key, values) in rowData)
            {
                try
                {
                    var dto = _parser.ReadRow(values);
                    result.Add(key, dto);
                }
                catch (InvalidRowDataException ex)
                {
                    Reporter.ReportError(Entity, key.IdHash, ex);
                }
            }

            return result;
        }

        protected abstract DbSet<TEntity> DbSet { get; }

        protected abstract Entity Entity { get; }

        protected virtual List<TEntity> AttachRelatedEntities(List<TEntity> entities)
        {
            return entities;
        }

        protected virtual void DeleteOrphans()
        {
        }
    }
}