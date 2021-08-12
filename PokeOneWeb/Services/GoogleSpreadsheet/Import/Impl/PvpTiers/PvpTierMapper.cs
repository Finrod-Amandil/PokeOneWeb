using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.PvpTiers
{
    public class PvpTierMapper : ISpreadsheetEntityMapper<PvpTierDto, PvpTier>
    {
        private readonly ILogger<PvpTierMapper> _logger;

        public PvpTierMapper(ILogger<PvpTierMapper> logger)
        {
            _logger = logger;
        } 
        public IEnumerable<PvpTier> Map(IDictionary<RowHash, PvpTierDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid PvpTier DTO. Skipping.");
                    continue;
                }

                yield return MapPvpTier(dto, rowHash);
            }
        }

        public IEnumerable<PvpTier> MapOnto(IList<PvpTier> entities, IDictionary<RowHash, PvpTierDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid PvpTier DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching PvpTier entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapPvpTier(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private bool IsValid(PvpTierDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        private PvpTier MapPvpTier(PvpTierDto dto, RowHash rowHash, PvpTier pvpTier = null)
        {
            pvpTier ??= new PvpTier();

            pvpTier.IdHash = rowHash.IdHash;
            pvpTier.Hash = rowHash.ContentHash;
            pvpTier.ImportSheetId = rowHash.ImportSheetId;
            pvpTier.Name = dto.Name;
            pvpTier.SortIndex = dto.SortIndex;

            return pvpTier;
        }
    }
}
