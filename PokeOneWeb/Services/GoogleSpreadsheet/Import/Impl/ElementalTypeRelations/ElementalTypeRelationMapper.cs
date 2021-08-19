using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.ElementalTypeRelations
{
    public class ElementalTypeRelationMapper : ISpreadsheetEntityMapper<ElementalTypeRelationSheetDto, ElementalTypeRelation>
    {
        private readonly ILogger<ElementalTypeRelationMapper> _logger;

        private IDictionary<string, ElementalType> _types;

        public ElementalTypeRelationMapper(ILogger<ElementalTypeRelationMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<ElementalTypeRelation> Map(IDictionary<RowHash, ElementalTypeRelationSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _types = new Dictionary<string, ElementalType>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid ElementalTypeRelation DTO. Skipping.");
                    continue;
                }

                yield return MapElementalTypeRelation(dto, rowHash);
            }
        }

        public IEnumerable<ElementalTypeRelation> MapOnto(IList<ElementalTypeRelation> entities, IDictionary<RowHash, ElementalTypeRelationSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _types = new Dictionary<string, ElementalType>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid ElementalTypeRelation DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching ElementalTypeRelation entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapElementalTypeRelation(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private bool IsValid(ElementalTypeRelationSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.AttackingTypeName) &&
                !string.IsNullOrWhiteSpace(dto.DefendingTypeName);
        }

        private ElementalTypeRelation MapElementalTypeRelation(
            ElementalTypeRelationSheetDto dto, 
            RowHash rowHash,
            ElementalTypeRelation elementalTypeRelation = null)
        {
            elementalTypeRelation ??= new ElementalTypeRelation();

            ElementalType attackingType;
            if (_types.ContainsKey(dto.AttackingTypeName))
            {
                attackingType = _types[dto.AttackingTypeName];
            }
            else
            {
                attackingType = new ElementalType { Name = dto.AttackingTypeName };
                _types.Add(dto.AttackingTypeName, attackingType);
            }

            ElementalType defendingType;
            if (_types.ContainsKey(dto.DefendingTypeName))
            {
                defendingType = _types[dto.DefendingTypeName];
            }
            else
            {
                defendingType = new ElementalType { Name = dto.DefendingTypeName };
                _types.Add(dto.DefendingTypeName, defendingType);
            }

            elementalTypeRelation.IdHash = rowHash.IdHash;
            elementalTypeRelation.Hash = rowHash.ContentHash;
            elementalTypeRelation.ImportSheetId = rowHash.ImportSheetId;
            elementalTypeRelation.AttackingType = attackingType;
            elementalTypeRelation.DefendingType = defendingType;
            elementalTypeRelation.AttackEffectivity = dto.Effectivity;

            return elementalTypeRelation;
        }
    }
}
