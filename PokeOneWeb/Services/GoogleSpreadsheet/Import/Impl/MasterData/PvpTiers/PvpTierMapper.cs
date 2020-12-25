using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.PvpTiers
{
    public class PvpTierMapper : ISpreadsheetEntityMapper<PvpTierDto, PvpTier>
    {
        private readonly ILogger<PvpTierMapper> _logger;

        public PvpTierMapper(ILogger<PvpTierMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<PvpTier> Map(IEnumerable<PvpTierDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid PvpTier DTO. Skipping.");
                    continue;
                }

                yield return new PvpTier
                {
                    Name = dto.Name,
                    SortIndex = dto.SortIndex
                };
            }
        }

        private bool IsValid(PvpTierDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }
    }
}
