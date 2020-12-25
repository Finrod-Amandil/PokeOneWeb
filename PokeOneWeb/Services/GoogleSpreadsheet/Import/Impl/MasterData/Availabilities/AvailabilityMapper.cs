using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Availabilities
{
    public class AvailabilityMapper : ISpreadsheetEntityMapper<AvailabilityDto, PokemonAvailability>
    {
        private readonly ILogger<AvailabilityMapper> _logger;

        public AvailabilityMapper(ILogger<AvailabilityMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<PokemonAvailability> Map(IEnumerable<AvailabilityDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning("Found invalid Availability DTO. Skipping.");
                    continue;
                }

                yield return new PokemonAvailability
                {
                    Name = dto.Name,
                    Description = dto.Description
                };
            }
        }

        private bool IsValid(AvailabilityDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.Name) &&
                !string.IsNullOrWhiteSpace(dto.Description);
        }
    }
}
