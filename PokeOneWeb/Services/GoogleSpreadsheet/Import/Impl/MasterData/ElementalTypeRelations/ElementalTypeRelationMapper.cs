using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.ElementalTypeRelations
{
    public class ElementalTypeRelationMapper : ISpreadsheetEntityMapper<ElementalTypeRelationDto, ElementalTypeRelation>
    {
        private readonly ILogger<ElementalTypeRelationMapper> _logger;

        private IDictionary<string, ElementalType> _types;

        public ElementalTypeRelationMapper(ILogger<ElementalTypeRelationMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<ElementalTypeRelation> Map(IEnumerable<ElementalTypeRelationDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            _types = new Dictionary<string, ElementalType>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning("Found invalid Elemental Type Relation DTO. Skipping.");
                    continue;
                }

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

                yield return new ElementalTypeRelation
                {
                    AttackingType = attackingType,
                    DefendingType = defendingType,
                    AttackEffectivity = dto.Effectivity
                };
            }
        }

        private bool IsValid(ElementalTypeRelationDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.AttackingTypeName) &&
                !string.IsNullOrWhiteSpace(dto.DefendingTypeName);
        }
    }
}
