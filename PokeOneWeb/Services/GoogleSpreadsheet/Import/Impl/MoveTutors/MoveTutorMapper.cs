using System;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MoveTutors
{
    public class MoveTutorMapper : ISpreadsheetEntityMapper<MoveTutorSheetDto, MoveTutor>
    {
        private readonly ILogger<MoveTutorMapper> _logger;

        private IDictionary<string, Location> _locations;

        public MoveTutorMapper(ILogger<MoveTutorMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<MoveTutor> Map(IDictionary<RowHash, MoveTutorSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _locations = new Dictionary<string, Location>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid MoveTutor DTO. Skipping.");
                    continue;
                }

                yield return MapMoveTutor(dto, rowHash);
            }
        }

        public IEnumerable<MoveTutor> MapOnto(IList<MoveTutor> entities, IDictionary<RowHash, MoveTutorSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _locations = new Dictionary<string, Location>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid MoveTutor DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching MoveTutor entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapMoveTutor(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private bool IsValid(MoveTutorSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.Name) &&
                !string.IsNullOrWhiteSpace(dto.LocationName);
        }

        private MoveTutor MapMoveTutor(MoveTutorSheetDto dto, RowHash rowHash, MoveTutor moveTutor = null)
        {
            moveTutor ??= new MoveTutor();

            Location location;
            if (_locations.ContainsKey(dto.LocationName))
            {
                location = _locations[dto.LocationName];
            }
            else
            {
                location = new Location { Name = dto.LocationName };
                _locations.Add(dto.LocationName, location);
            }

            moveTutor.IdHash = rowHash.IdHash;
            moveTutor.Hash = rowHash.ContentHash;
            moveTutor.ImportSheetId = rowHash.ImportSheetId;
            moveTutor.Name = dto.Name;
            moveTutor.PlacementDescription = dto.PlacementDescription;
            moveTutor.Location = location;

            return moveTutor;
        }
    }
}
