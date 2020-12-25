using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Moves
{
    public class MoveMapper : ISpreadsheetEntityMapper<MoveDto, Move>
    {
        private readonly ILogger<MoveMapper> _logger;

        private IDictionary<string, MoveDamageClass> _damageClasses;
        private IDictionary<string, ElementalType> _types;

        public MoveMapper(ILogger<MoveMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Move> Map(IEnumerable<MoveDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            _damageClasses = new Dictionary<string, MoveDamageClass>();
            _types = new Dictionary<string, ElementalType>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Move DTO with item name {dto.Name}. Skipping.");
                    continue;
                }

                MoveDamageClass damageClass;
                if (_damageClasses.ContainsKey(dto.DamageClassName))
                {
                    damageClass = _damageClasses[dto.DamageClassName];
                }
                else
                {
                    damageClass = new MoveDamageClass { Name = dto.DamageClassName };
                    _damageClasses.Add(dto.DamageClassName, damageClass);
                }

                ElementalType type;
                if (_types.ContainsKey(dto.TypeName))
                {
                    type = _types[dto.TypeName];
                }
                else
                {
                    type = new ElementalType { Name = dto.TypeName };
                    _types.Add(dto.TypeName, type);
                }

                yield return new Move
                {
                    Name = dto.Name,
                    DoInclude = dto.DoInclude,
                    ResourceName = dto.ResourceName,
                    DamageClass = damageClass,
                    ElementalType = type,
                    AttackPower = dto.AttackPower,
                    Accuracy = dto.Accuracy,
                    PowerPoints = dto.PowerPoints,
                    Priority = dto.Priority,
                    PokeApiName = dto.PokeApiName,
                    Effect = dto.Effect
                };
            }
        }

        private bool IsValid(MoveDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.Name) &&
                !string.IsNullOrWhiteSpace(dto.ResourceName) &&
                !string.IsNullOrWhiteSpace(dto.DamageClassName) &&
                !string.IsNullOrWhiteSpace(dto.TypeName);
        }
    }
}
