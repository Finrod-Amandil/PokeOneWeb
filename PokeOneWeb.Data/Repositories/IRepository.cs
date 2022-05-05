using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Repositories.Impl;

namespace PokeOneWeb.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        public event EventHandler<UpdateOrInsertExceptionOccurredEventArgs> UpdateOrInsertExceptionOccurred;

        int Insert(ICollection<TEntity> entities);

        bool Insert(TEntity entity);

        int Update(ICollection<TEntity> entities);

        bool Update(TEntity entity);
    }
}