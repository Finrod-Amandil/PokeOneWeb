using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Exceptions;

namespace PokeOneWeb.Data.Repositories.Impl
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        public event EventHandler<UpdateOrInsertExceptionOccurredEventArgs> UpdateOrInsertExceptionOccurred;

        public virtual int Insert(ICollection<TEntity> entities)
        {
            entities = PrepareEntitiesForInsertOrUpdate(entities);
            DbContext.Set<TEntity>().AddRange(entities);
            DbContext.SaveChanges();

            // Clear change tracker to avoid tracking the same entity twice.
            DbContext.ChangeTracker.Clear();

            return entities.Count;
        }

        public bool Insert(TEntity entity)
        {
            var insertedCount = Insert(new List<TEntity> { entity });
            return insertedCount > 0;
        }

        public virtual int Update(ICollection<TEntity> entities)
        {
            entities = PrepareEntitiesForInsertOrUpdate(entities);
            DbContext.Set<TEntity>().UpdateRange(entities);
            DbContext.SaveChanges();

            // Clear change tracker to avoid tracking the same entity twice.
            DbContext.ChangeTracker.Clear();

            return entities.Count;
        }

        public bool Update(TEntity entity)
        {
            var updatedCount = Update(new List<TEntity> { entity });
            return updatedCount > 0;
        }

        protected readonly ApplicationDbContext DbContext;

        /// <summary>
        /// Actions to perform on every entity before it is inserted or updated. Return
        /// false if the action is unsuccessful and the entity should not be inserted or
        /// updated, return true if the preparation step could be performed successfully.
        /// </summary>
        protected virtual List<Func<TEntity, bool>> PreparationSteps => new();

        protected Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <summary>
        /// Prepares the entity before inserting or updating, such as resolving
        /// relations to related entities.
        /// </summary>
        /// <param name="entities">The entities to insert or update</param>
        /// <returns>The set of entities for which preparation was successful
        /// and which should still be inserted or updated.</returns>
        protected virtual ICollection<TEntity> PrepareEntitiesForInsertOrUpdate(ICollection<TEntity> entities)
        {
            var verifiedEntities = new List<TEntity>(entities);
            foreach (var entity in entities)
            {
                var canBeInsertedOrUpdated = true;
                foreach (var preparationStep in PreparationSteps)
                {
                    canBeInsertedOrUpdated &= preparationStep(entity);
                }

                // If any preparation step was not successful, skip entity.
                if (!canBeInsertedOrUpdated)
                {
                    verifiedEntities.Remove(entity);
                }
            }

            return verifiedEntities;
        }

        /// <summary>
        /// Tries to find the id of the named entity in the data store which matches
        /// the given name.
        /// </summary>
        /// <typeparam name="TNamedEntity">Which type of entity should be found.</typeparam>
        /// <param name="entityName">The name of the entity to be looked up.</param>
        /// <returns>The id of the existing entity of the given type that matches
        /// the given name. If no such entity is found, null is returned.</returns>
        protected int? GetOptionalIdForName<TNamedEntity>(string entityName) where TNamedEntity : class, INamedEntity
        {
            if (string.IsNullOrWhiteSpace(entityName))
            {
                return null;
            }

            var id = DbContext.Set<TNamedEntity>()
                .AsNoTracking()
                .Where(x => x.Name.Equals(entityName))
                .Select(x => x.Id)
                .FirstOrDefault();

            return id > 0 ? id : null;
        }

        /// <summary>
        /// Attempts to look up, and, if successful, set the id for a related entity by a given name and
        /// type.
        /// </summary>
        /// <typeparam name="TNamedEntity">Which type of entity should be found.</typeparam>
        /// <param name="entityName">The name of the entity to be looked up.</param>
        /// <param name="setId">The action that should be executed with the found id,
        /// if a matching id could be found.</param>
        /// <returns>True, if a matching entity could be found, false otherwise.</returns>
        protected bool TrySetIdForName<TNamedEntity>(string entityName, Action<int> setId) where TNamedEntity : class, INamedEntity
        {
            var id = GetOptionalIdForName<TNamedEntity>(entityName);

            if (id is null)
            {
                var exception = new RelatedEntityNotFoundException(typeof(TEntity).Name, typeof(TNamedEntity).Name, entityName);
                ReportInsertOrUpdateException(typeof(TNamedEntity), exception);
            }
            else
            {
                setId((int)id);
            }

            return id is not null;
        }

        /// <summary>
        /// Adds or updates a set of related entities, depending on whether an entity of the given type and name
        /// already exists in the data store or not.
        /// </summary>
        /// <typeparam name="TRelatedEntity">Which type of entity should be added/updated.</typeparam>
        /// <param name="entities">The named entities to add or update.</param>
        protected void AddOrUpdateRelatedEntitiesByName<TRelatedEntity>(IEnumerable<TRelatedEntity> entities) where TRelatedEntity : class, INamedEntity
        {
            var distinctEntities = entities.DistinctBy(x => x.Name).ToList();

            foreach (var entity in distinctEntities)
            {
                entity.Id = GetOptionalIdForName<TRelatedEntity>(entity.Name) ?? 0;
            }

            DbContext.Set<TRelatedEntity>().UpdateRange(distinctEntities);
            DbContext.SaveChanges();
        }

        /// <summary>
        /// Reports an error as an event. This can be used to report that some entities could not be processed,
        /// but still process the other entities.
        /// </summary>
        /// <param name="entityType">The type of the entity that failed to be processed.</param>
        /// <param name="exception">An exception describing the problem that occurred.</param>
        protected void ReportInsertOrUpdateException(Type entityType, Exception exception)
        {
            UpdateOrInsertExceptionOccurred?.Invoke(this, new UpdateOrInsertExceptionOccurredEventArgs(entityType, exception));
        }
    }
}