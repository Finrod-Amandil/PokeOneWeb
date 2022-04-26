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

        protected virtual void AddIdsForNames(TEntity entity)
        {
            // Override this method in entity repositories, if any related entities need to be attached by name.
            // Pattern: entity.RelatedEntityId = GetIdForName<RelatedEntity>(entity.RelatedEntityName);
        }

        protected int? GetOptionalIdForName<TNamedEntity>(string entityName) where TNamedEntity : class, INamedEntity
        {
            if (string.IsNullOrWhiteSpace(entityName))
            {
                return null;
            }

            var typeName = typeof(TNamedEntity).FullName ?? throw new Exception();

            if (!_idsForEntityNames.ContainsKey(typeName))
            {
                _idsForEntityNames.Add(typeName, DbContext.Set<TNamedEntity>()
                    .ToDictionary(x => x.Name, x => x.Id));
            }

            return _idsForEntityNames[typeName].TryGetValue(entityName, out var id) ? id : null;
        }

        protected int GetRequiredIdForName<TNamedEntity>(string entityName) where TNamedEntity : class, INamedEntity
        {
            var id = GetOptionalIdForName<TNamedEntity>(entityName);

            if (id is null)
            {
                throw new RelatedEntityNotFoundException(
                    typeof(TEntity).Name, typeof(TNamedEntity).Name, entityName);
            }

            return (int)id;
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
                entity.Id = GetOptionalIdForName<TRelatedEntity>(entity.Name);
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