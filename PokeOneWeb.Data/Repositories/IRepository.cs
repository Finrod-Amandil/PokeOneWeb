using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Repositories.Impl;

namespace PokeOneWeb.Data.Repositories
{
    /// <summary>
    /// Base repository that offers CRUD methods that are common to all entities.
    /// </summary>
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        public event EventHandler<UpdateOrInsertExceptionOccurredEventArgs> UpdateOrInsertExceptionOccurred;

        /// <summary>
        /// Inserts multiple entities into the data store.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        /// <returns>How many entities were successfully inserted. Subscribe to the
        /// UpdateOrInsertExceptionOccurred event to get information about which
        /// entities were skipped and why.</returns>
        int Insert(ICollection<TEntity> entities);

        /// <summary>
        /// Inserts a single entity into the data store.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <returns>Whether the insertion was successful. Subscribe to the
        /// UpdateOrInsertExceptionOccurred event to get information about why
        /// the insertion failed.</returns>
        bool Insert(TEntity entity);

        /// <summary>
        /// Updates multiple existing entities in the data store.
        /// </summary>
        /// <param name="entities">The entities to be updated.</param>
        /// <returns>How many entities were successfully updated. Subscribe to the
        /// UpdateOrInsertExceptionOccurred event to get information about which
        /// entities were skipped and why.</returns>
        int Update(ICollection<TEntity> entities);

        /// <summary>
        /// Updates a single entity in the data store.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>Whether the update was successful. Subscribe to the
        /// UpdateOrInsertExceptionOccurred event to get information about why
        /// the update failed.</returns>
        bool Update(TEntity entity);
    }
}