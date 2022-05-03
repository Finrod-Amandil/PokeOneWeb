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

        protected readonly ApplicationDbContext DbContext;

        protected Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual void Insert(ICollection<TEntity> entities)
        {
            PrepareEntitiesForInsertOrUpdate(entities);
            DbContext.Set<TEntity>().AddRange(entities);
            DbContext.SaveChanges();
        }

        public void Insert(TEntity entity)
        {
            Insert(new List<TEntity> { entity });
        }

        public virtual void Update(ICollection<TEntity> entities)
        {
            PrepareEntitiesForInsertOrUpdate(entities);
            DbContext.Set<TEntity>().UpdateRange(entities);
            DbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            Update(new List<TEntity> { entity });
        }

        protected virtual void PrepareEntitiesForInsertOrUpdate(ICollection<TEntity> entity)
        {
        }

        protected int? GetOptionalIdForName<TNamedEntity>(string entityName) where TNamedEntity : class, INamedEntity
        {
            if (string.IsNullOrWhiteSpace(entityName))
            {
                return null;
            }

            var id = DbContext.Set<TNamedEntity>()
                .Where(x => x.Name.Equals(entityName))
                .Select(x => x.Id)
                .FirstOrDefault();

            return id;
        }

        protected int GetRequiredIdForName<TNamedEntity>(string entityName) where TNamedEntity : class, INamedEntity
        {
            var id = GetOptionalIdForName<TNamedEntity>(entityName);

            if (id is null)
            {
                var exception = new RelatedEntityNotFoundException(typeof(TEntity).Name, typeof(TNamedEntity).Name, entityName);
                ReportInsertOrUpdateException(typeof(TNamedEntity), exception);
                throw exception;
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

        protected void ReportInsertOrUpdateException(Type entityType, Exception exception)
        {
            UpdateOrInsertExceptionOccurred?.Invoke(this, new UpdateOrInsertExceptionOccurredEventArgs(entityType, exception));
        }
    }
}