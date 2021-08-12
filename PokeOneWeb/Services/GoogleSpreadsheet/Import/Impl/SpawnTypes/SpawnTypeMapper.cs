using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.SpawnTypes
{
    public class SpawnTypeMapper : ISpreadsheetEntityMapper<SpawnTypeDto, SpawnType>
    {
        private readonly ILogger<SpawnTypeMapper> _logger;

        public SpawnTypeMapper(ILogger<SpawnTypeMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<SpawnType> Map(IDictionary<RowHash, SpawnTypeDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning("Found invalid SpawnType DTO. Skipping.");
                    continue;
                }

                yield return MapSpawnType(dto, rowHash);
            }
        }

        public IEnumerable<SpawnType> MapOnto(IList<SpawnType> entities, IDictionary<RowHash, SpawnTypeDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid SpawnType DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching SpawnType entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapSpawnType(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private bool IsValid(SpawnTypeDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        private SpawnType MapSpawnType(SpawnTypeDto dto, RowHash rowHash, SpawnType spawnType = null)
        {
            spawnType ??= new SpawnType();

            spawnType.IdHash = rowHash.IdHash;
            spawnType.Hash = rowHash.ContentHash;
            spawnType.ImportSheetId = rowHash.ImportSheetId;
            spawnType.Name = dto.Name;
            spawnType.SortIndex = dto.SortIndex;
            spawnType.IsSyncable = dto.IsSyncable;
            spawnType.IsInfinite = dto.IsInfinite;
            spawnType.Color = dto.Color;

            return spawnType;
        }
    }
}
