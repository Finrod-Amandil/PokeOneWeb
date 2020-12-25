using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.ElementalTypes
{
    public class ElementalTypeMapper : ISpreadsheetEntityMapper<ElementalTypeDto, ElementalType>
    {
        private readonly ILogger<ElementalTypeMapper> _logger;

        public ElementalTypeMapper(ILogger<ElementalTypeMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<ElementalType> Map(IEnumerable<ElementalTypeDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Elemental Type DTO. Skipping.");
                    continue;
                }

                yield return new ElementalType
                {
                    Name = dto.Name,
                    PokeApiName = dto.PokeApiName
                };
            }
        }

        private bool IsValid(ElementalTypeDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }
    }
}
