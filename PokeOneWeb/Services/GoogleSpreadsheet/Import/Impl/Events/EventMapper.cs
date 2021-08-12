using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Events
{
    public class EventMapper : ISpreadsheetEntityMapper<EventDto, Event>
    {
        private readonly ILogger<EventMapper> _logger;

        public EventMapper(ILogger<EventMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Event> Map(IDictionary<RowHash, EventDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning("Found invalid Event DTO. Skipping.");
                    continue;
                }

                yield return MapEvent(dto, rowHash);
            }
        }

        public IEnumerable<Event> MapOnto(IList<Event> entities, IDictionary<RowHash, EventDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Event DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching Event entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapEvent(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private static bool IsValid(EventDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        private Event MapEvent(EventDto dto, RowHash rowHash, Event eventEntity = null)
        {
            eventEntity ??= new Event();

            eventEntity.IdHash = rowHash.IdHash;
            eventEntity.Hash = rowHash.ContentHash;
            eventEntity.ImportSheetId = rowHash.ImportSheetId;
            eventEntity.Name = dto.Name;
            eventEntity.StartDate = dto.StartDate;
            eventEntity.EndDate = dto.EndDate;

            return eventEntity;
        }
    }
}
