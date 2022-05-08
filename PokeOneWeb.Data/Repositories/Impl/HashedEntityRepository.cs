using System;
using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Data.Entities.Interfaces;

namespace PokeOneWeb.Data.Repositories.Impl
{
    public abstract class HashedEntityRepository<TEntity> : Repository<TEntity>, IHashedEntityRepository<TEntity> where TEntity : class, IHashedEntity
    {
        protected HashedEntityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<RowHash> GetHashesForSheet(ImportSheet sheet)
        {
            var rowHashes = DbContext.Set<TEntity>()
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash
                {
                    Hash = x.Hash,
                    IdHash = x.IdHash,
                    ImportSheetId = x.ImportSheetId
                })
                .ToList();

            return rowHashes;
        }

        public virtual int DeleteByIdHashes(ICollection<string> idHashes)
        {
            var deletedCount = DbContext.Set<TEntity>()
                .Where(x => idHashes.Contains(x.IdHash))
                .DeleteFromQuery();

            return deletedCount;
        }

        public virtual int UpdateByIdHashes(ICollection<TEntity> entities)
        {
            var idsForHashes = DbContext.Set<TEntity>()
                .Select(x => new { x.Id, x.IdHash })
                .ToDictionary(x => x.IdHash, x => x.Id);

            // Set ID for all entities
            foreach (var entity in entities)
            {
                if (!idsForHashes.ContainsKey(entity.IdHash))
                {
                    var exception = new ArgumentOutOfRangeException(
                        $"No matching {typeof(TEntity).Name} could be found for IdHash {entity.IdHash}, skipping update.");

                    ReportInsertOrUpdateException(typeof(TEntity), exception);
                }

                entity.Id = idsForHashes[entity.IdHash];
            }

            return Update(entities);
        }
    }
}