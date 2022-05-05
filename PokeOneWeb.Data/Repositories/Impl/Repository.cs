using System;
using System.Collections.Generic;
using System.Linq;
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

            return entities.Count;
        }

        public bool Update(TEntity entity)
        {
            var updatedCount = Update(new List<TEntity> { entity });
            return updatedCount > 0;
        }

        protected readonly ApplicationDbContext DbContext;

        protected virtual List<Func<TEntity, bool>> PreparationSteps => new();

        protected Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

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

                if (!canBeInsertedOrUpdated)
                {
                    verifiedEntities.Remove(entity);
                }
            }

            return verifiedEntities;
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

        protected void ReportInsertOrUpdateException(Type entityType, Exception exception)
        {
            UpdateOrInsertExceptionOccurred?.Invoke(this, new UpdateOrInsertExceptionOccurredEventArgs(entityType, exception));
        }
    }
}