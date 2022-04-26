using System;
using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Exceptions;

namespace PokeOneWeb.Data.Repositories.Impl
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly Dictionary<string, Dictionary<string, int>> _idsForEntityNames = new();

        protected readonly ApplicationDbContext DbContext;

        protected Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual void Insert(ICollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                AddIdsForNames(entity);
            }

            DbContext.Set<TEntity>().AddRange(entities);
            DbContext.SaveChanges();
        }

        public virtual void Update(ICollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                AddIdsForNames(entity);
            }

            DbContext.Set<TEntity>().UpdateRange(entities);
            DbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            AddIdsForNames(entity);
            DbContext.Set<TEntity>().Update(entity);
            DbContext.SaveChanges();
        }

        private AggregateException PrepareEntitiesAndAggregateExceptions(ICollection<TEntity> entities)
        {
            var exceptions = new List<Exception>();

            foreach (var entity in entities)
            {
                try
                {
                    AddIdsForNames(entity);
                }
                catch (RelatedEntityNotFoundException e)
                {
                    exceptions.Add(e);
                }
            }

            return exceptions.Any() ? new AggregateException(exceptions) : null;
        }

        protected virtual void AddIdsForNames(TEntity entity)
        {
            // Override this method in entity repositories, if any related entities need to be attached by name.
            // Pattern: entity.RelatedEntityId = GetIdForName<RelatedEntity>(entity.RelatedEntityName);
        }

        /// <summary>
        /// Looks up the Id of an entity of the given type by looking for an entity which has the given name.
        /// Returns 0 if no matching entity was found.
        /// </summary>
        protected int GetIdForName<TNamedEntity>(string entityName) where TNamedEntity : class, INamedEntity
        {
            if (string.IsNullOrWhiteSpace(entityName))
            {
                return 0;
            }

            var typeName = typeof(TNamedEntity).FullName ?? throw new Exception();

            if (!_idsForEntityNames.ContainsKey(typeName))
            {
                _idsForEntityNames.Add(typeName, DbContext.Set<TNamedEntity>()
                    .ToDictionary(x => x.Name, x => x.Id));
            }

            return _idsForEntityNames[typeName].TryGetValue(entityName, out var id) ? id : 0;
        }

        protected int GetRequiredIdForName<TNamedEntity>(string entityName) where TNamedEntity : class, INamedEntity
        {
            var id = GetIdForName<TNamedEntity>(entityName);

            if (id == 0)
            {
                throw new RelatedEntityNotFoundException(
                    typeof(TEntity).Name, typeof(TNamedEntity).Name, entityName);
            }

            return id;
        }

        /// <summary>
        /// Adds a collection of entities to the database upon which the "core" entities being inserted/updated depend upon
        /// and which are not tracked by hashes. The dependent entities require a unique name to check whether they already
        /// exist in the DB. If not, they are inserted, else the existing entities are updated.
        /// </summary>
        protected void AddOrUpdateRelatedEntitiesByName<TRelatedEntity>(IEnumerable<TRelatedEntity> entities) where TRelatedEntity : class, INamedEntity
        {
            var distinctEntities = entities.DistinctBy(x => x.Name).ToList();

            foreach (var entity in distinctEntities)
            {
                entity.Id = GetIdForName<TRelatedEntity>(entity.Name);
            }

            DbContext.Set<TRelatedEntity>().UpdateRange(distinctEntities);
            DbContext.SaveChanges();
        }

        /// <summary>
        /// Deletes all related entities, to which the "core" entities being inserted/updated are children and which have no more
        /// children after the insertions/updates. Make sure all changes have been saved to the DB before calling this method.
        /// </summary>
        /// <param name="getCollectionOfChildren">Selector for the collection of child entities.</param>
        protected void DeleteUnusedParentalRelatedEntities<TRelatedEntity, TChildEntity>(
            Func<TRelatedEntity, ICollection<TChildEntity>> getCollectionOfChildren) where TRelatedEntity : class, IEntity
        {
            DbContext.Set<TRelatedEntity>().Where(x => !getCollectionOfChildren(x).Any()).DeleteFromQuery();
        }
    }
}