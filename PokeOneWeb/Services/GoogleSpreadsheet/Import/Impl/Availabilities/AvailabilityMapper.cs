using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Availabilities
{
    public class AvailabilityMapper : ISpreadsheetEntityMapper<AvailabilityDto, PokemonAvailability>
    {
        private readonly ILogger<AvailabilityMapper> _logger;

        public AvailabilityMapper(ILogger<AvailabilityMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<PokemonAvailability> Map(IDictionary<RowHash, AvailabilityDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning("Found invalid Availability DTO. Skipping.");
                    continue;
                }

                yield return MapAvailability(dto, rowHash);
            }
        }

        public IEnumerable<PokemonAvailability> MapOnto(IList<PokemonAvailability> entities, IDictionary<RowHash, AvailabilityDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Availability DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching Availability entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }
                
                MapAvailability(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private bool IsValid(AvailabilityDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.Name) &&
                !string.IsNullOrWhiteSpace(dto.Description);
        }

        private PokemonAvailability MapAvailability(
            AvailabilityDto dto, 
            RowHash rowHash,
            PokemonAvailability availability = null)
        {
            availability ??= new PokemonAvailability();

            availability.IdHash = rowHash.IdHash;
            availability.Hash = rowHash.ContentHash;
            availability.ImportSheetId = rowHash.ImportSheetId;
            availability.Name = dto.Name;
            availability.Description = dto.Description;

            return availability;
        }
    }
}
