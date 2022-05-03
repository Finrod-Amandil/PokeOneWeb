using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Repositories.Impl;

namespace PokeOneWeb.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        public event EventHandler<UpdateOrInsertExceptionOccurredEventArgs> UpdateOrInsertExceptionOccurred;

        void Insert(ICollection<TEntity> entities);

        void Update(ICollection<TEntity> entities);

        void Update(TEntity entity);
    }
}