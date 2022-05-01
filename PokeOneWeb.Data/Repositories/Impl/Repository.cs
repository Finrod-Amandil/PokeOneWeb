using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        public void Insert(TEntity entity)
        {
            AddIdsForNames(entity);
            DbContext.Set<TEntity>().Add(entity);
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

            var typeName = typeof(TNamedEntity).Name;

            if (!_idsForEntityNames.ContainsKey(typeName) ||
                DbContext.Set<TNamedEntity>().Count() != _idsForEntityNames[typeName].Count)
            {
                _idsForEntityNames[typeName] = DbContext.Set<TNamedEntity>()
                    .AsNoTracking()
                    .ToDictionary(x => x.Name, x => x.Id);
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

        protected void AddOrUpdateRelatedEntitiesByName<TRelatedEntity>(IEnumerable<TRelatedEntity> entities) where TRelatedEntity : class, INamedEntity
        {
            var distinctEntities = entities.DistinctBy(x => x.Name).ToList();

            var existingRelatedEntities = DbContext.Set<TRelatedEntity>()
                .AsNoTracking()
                .ToDictionary(x => x.Name, x => x.Id);

            foreach (var entity in distinctEntities)
            {
                // If entity exists, find Id so that EF updates the corresponding entry.
                // Else set Id to zero, which tells EF to treat it as new entry.
                entity.Id = existingRelatedEntities.TryGetValue(entity.Name, out var id) ? id : 0;
            }

            DbContext.Set<TRelatedEntity>().UpdateRange(distinctEntities);
            DbContext.SaveChanges();
        }
    }
}