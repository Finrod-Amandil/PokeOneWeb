using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Regions
{
    public class RegionMapper : ISpreadsheetEntityMapper<RegionSheetDto, Region>
    {
        private readonly ILogger<RegionMapper> _logger;

        private IDictionary<string, Event> _events;

        public RegionMapper(ILogger<RegionMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Region> Map(IDictionary<RowHash, RegionSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _events = new Dictionary<string, Event>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Region DTO. Skipping.");
                    continue;
                }

                yield return MapRegion(dto, rowHash);
            }
        }

        public IEnumerable<Region> MapOnto(IList<Region> entities, IDictionary<RowHash, RegionSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _events = new Dictionary<string, Event>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Region DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching Region entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapRegion(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private bool IsValid(RegionSheetDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        private Region MapRegion(RegionSheetDto dto, RowHash rowHash, Region region = null)
        {
            region ??= new Region();

            if (!string.IsNullOrWhiteSpace(dto.EventName))
            {
                Event eventEntity;
                if (_events.ContainsKey(dto.EventName))
                {
                    eventEntity = _events[dto.EventName];
                }
                else
                {
                    eventEntity = new Event { Name = dto.EventName };
                    _events.Add(dto.EventName, eventEntity);
                }
                region.Event = eventEntity;
            }

            region.IdHash = rowHash.IdHash;
            region.Hash = rowHash.ContentHash;
            region.ImportSheetId = rowHash.ImportSheetId;
            region.Name = dto.Name;
            region.Color = dto.Color;
            region.IsEventRegion = dto.IsEventRegion;

            return region;
        }
    }
}
