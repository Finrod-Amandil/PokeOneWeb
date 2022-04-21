using System.Collections.Generic;
using PokeOneWeb.Data.Entities.Interfaces;

namespace PokeOneWeb.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        void Insert(ICollection<TEntity> entities);

        void Insert(TEntity entity);

        void Update(ICollection<TEntity> entities);

        void Update(TEntity entity);
    }
}