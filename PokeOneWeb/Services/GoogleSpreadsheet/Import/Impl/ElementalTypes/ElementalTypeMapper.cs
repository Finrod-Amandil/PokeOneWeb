using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.ElementalTypes
{
    public class ElementalTypeMapper : ISpreadsheetEntityMapper<ElementalTypeDto, ElementalType>
    {
        private readonly ILogger<ElementalTypeMapper> _logger;

        public ElementalTypeMapper(ILogger<ElementalTypeMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<ElementalType> Map(IDictionary<RowHash, ElementalTypeDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Elemental Type DTO. Skipping.");
                    continue;
                }

                yield return MapElementalType(dto, rowHash);
            }
        }

        public IEnumerable<ElementalType> MapOnto(IList<ElementalType> entities, IDictionary<RowHash, ElementalTypeDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Elemental Type DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching Elemental Type entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapElementalType(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private bool IsValid(ElementalTypeDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        private ElementalType MapElementalType(ElementalTypeDto dto, RowHash rowHash, ElementalType elementalType = null)
        {
            elementalType ??= new ElementalType();

            elementalType.IdHash = rowHash.IdHash;
            elementalType.Hash = rowHash.ContentHash;
            elementalType.ImportSheetId = rowHash.ImportSheetId;
            elementalType.Name = dto.Name;
            elementalType.PokeApiName = dto.PokeApiName;

            return elementalType;
        }
    }
}
