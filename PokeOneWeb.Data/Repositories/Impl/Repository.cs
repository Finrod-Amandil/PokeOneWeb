using System;
using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data.Entities.Interfaces;

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
                DbContext.Set<TEntity>().Add(entity);
            }

            DbContext.SaveChanges();
        }

        public virtual void Insert(TEntity entity)
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
                DbContext.Set<TEntity>().Update(entity);
            }

            DbContext.SaveChanges();
        }

        public virtual void Update(TEntity entity)
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

        protected int GetIdForName<TNamedEntity>(string entityName) where TNamedEntity : class, INamedEntity
        {
            if (string.IsNullOrWhiteSpace(entityName))
            {
                return 0;
            }

            var typeName = typeof(TNamedEntity).FullName;

            if (typeName is not null && !_idsForEntityNames.ContainsKey(typeName))
            {
                _idsForEntityNames.Add(typeName, DbContext.Set<TNamedEntity>()
                    .Select(x => new { x.Name, x.Id })
                    .ToDictionary(x => x.Name, x => x.Id));
            }

            var idsForNames = _idsForEntityNames[typeName];

            if (!idsForNames.ContainsKey(entityName))
            {
                throw new Exception();
            }

            return idsForNames[entityName];
        }
    }
}