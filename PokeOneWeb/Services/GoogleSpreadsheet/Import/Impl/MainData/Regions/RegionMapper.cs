using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.Regions
{
    public class RegionMapper : ISpreadsheetEntityMapper<RegionDto, Region>
    {
        private readonly ILogger<RegionMapper> _logger;

        public RegionMapper(ILogger<RegionMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Region> Map(IEnumerable<RegionDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Region DTO. Skipping.");
                    continue;
                }

                var region = new Region
                {
                    Name = dto.RegionName,
                    Color = dto.Color,
                    IsEventRegion = dto.IsEventRegion,
                };

                if (dto.IsEventRegion && !string.IsNullOrEmpty(dto.EventName))
                {
                    region.Event = new Event
                    {
                        Name = dto.EventName,
                        StartDate = dto.EventStart,
                        EndDate = dto.EventEnd
                    };
                }

                yield return region;
            }
        }

        private bool IsValid(RegionDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.RegionName);
        }
    }
}
