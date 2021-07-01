using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.SpawnTypes
{
    public class SpawnTypeMapper : ISpreadsheetEntityMapper<SpawnTypeDto, SpawnType>
    {
        private readonly ILogger<SpawnTypeMapper> _logger;

        public SpawnTypeMapper(ILogger<SpawnTypeMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<SpawnType> Map(IEnumerable<SpawnTypeDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid SpawnType DTO. Skipping.");
                    continue;
                }

                yield return new SpawnType
                {
                    Name = dto.Name,
                    SortIndex = dto.SortIndex,
                    IsSyncable = dto.IsSyncable,
                    IsInfinite = dto.IsInfinite,
                    Color = dto.Color
                };
            }
        }

        private bool IsValid(SpawnTypeDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }
    }
}
