using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Data.Entities.Interfaces;

namespace PokeOneWeb.Data.Repositories
{
    public interface IHashedEntityRepository<TEntity> : IRepository<TEntity> where TEntity : class, IHashedEntity
    {
        IEnumerable<RowHash> GetHashesForSheet(ImportSheet sheet);

        int DeleteByIdHashes(ICollection<string> idHashes);

        int UpdateByIdHashes(ICollection<TEntity> entities);
    }
}