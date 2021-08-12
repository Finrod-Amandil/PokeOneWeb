using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.BagCategories
{
    public class BagCategoryMapper : ISpreadsheetEntityMapper<BagCategoryDto, BagCategory>
    {
        private readonly ILogger<BagCategoryMapper> _logger;

        public BagCategoryMapper(ILogger<BagCategoryMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<BagCategory> Map(IDictionary<RowHash, BagCategoryDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning("Found invalid BagCategory DTO. Skipping.");
                    continue;
                }

                yield return MapBagCategory(dto, rowHash);
            }
        }

        public IEnumerable<BagCategory> MapOnto(IList<BagCategory> entities, IDictionary<RowHash, BagCategoryDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid BagCategory DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching BagCategory entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapBagCategory(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private static bool IsValid(BagCategoryDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        private static BagCategory MapBagCategory(BagCategoryDto dto, RowHash rowHash, BagCategory bagCategory = null)
        {
            bagCategory ??= new BagCategory();

            bagCategory.IdHash = rowHash.IdHash;
            bagCategory.Hash = rowHash.ContentHash;
            bagCategory.ImportSheetId = rowHash.ImportSheetId;
            bagCategory.Name = dto.Name;
            bagCategory.SortIndex = dto.SortIndex;

            return bagCategory;
        }
    }
}
