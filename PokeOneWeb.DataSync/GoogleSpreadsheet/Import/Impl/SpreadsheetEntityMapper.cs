using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl
{
    public abstract class SpreadsheetEntityMapper<TDto, TEntity> 
        : ISpreadsheetEntityMapper<TDto, TEntity>
        where TEntity : class, IHashedEntity
        where TDto : ISpreadsheetEntityDto
    {
        protected readonly ISpreadsheetImportReporter Reporter;

        protected SpreadsheetEntityMapper(ISpreadsheetImportReporter reporter)
        {
            Reporter = reporter;
        }

        public IEnumerable<TEntity> Map(IDictionary<RowHash, TDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            var uniqueNames = new HashSet<string>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    Reporter.ReportError(Entity, rowHash.IdHash, $"Found invalid {Entity} DTO. Skipping.");
                    continue;
                }

                var uniqueName = GetUniqueName(dto);
                if (uniqueName != null && uniqueNames.Contains(uniqueName))
                {
                    Reporter.ReportError(Entity, rowHash.IdHash, $"Found duplicate {Entity} '{uniqueName}'. Skipping duplicate.");
                    continue;
                }
                uniqueNames.Add(uniqueName);

                yield return MapEntity(dto, rowHash);
            }
        }

        public IEnumerable<TEntity> MapOnto(IList<TEntity> entities, IDictionary<RowHash, TDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    Reporter.ReportError(Entity, rowHash.IdHash, $"Found invalid {Entity} DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    Reporter.ReportError(Entity, rowHash.IdHash, $"Found no single matching {Entity} entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapEntity(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        protected abstract Entity Entity { get; }

        protected abstract bool IsValid(TDto dto);

        protected abstract string GetUniqueName(TDto dto);

        protected abstract TEntity MapEntity(TDto dto, RowHash rowHash, TEntity entity = default);
    }
}
