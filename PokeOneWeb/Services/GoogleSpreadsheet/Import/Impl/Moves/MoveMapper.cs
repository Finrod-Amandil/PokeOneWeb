using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Moves
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

        public IEnumerable<Move> Map(IDictionary<RowHash, MoveDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _damageClasses = new Dictionary<string, MoveDamageClass>();
            _types = new Dictionary<string, ElementalType>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Move DTO. Skipping.");
                    continue;
                }

                yield return MapMove(dto, rowHash);
            }
        }

        public IEnumerable<Move> MapOnto(IList<Move> entities, IDictionary<RowHash, MoveDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _damageClasses = new Dictionary<string, MoveDamageClass>();
            _types = new Dictionary<string, ElementalType>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Move DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching Move entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapMove(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private bool IsValid(MoveDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.Name) &&
                !string.IsNullOrWhiteSpace(dto.ResourceName) &&
                !string.IsNullOrWhiteSpace(dto.DamageClassName) &&
                !string.IsNullOrWhiteSpace(dto.TypeName);
        }

        private Move MapMove(MoveDto dto, RowHash rowHash, Move move = null)
        {
            move ??= new Move();

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

            move.IdHash = rowHash.IdHash;
            move.Hash = rowHash.ContentHash;
            move.ImportSheetId = rowHash.ImportSheetId;
            move.Name = dto.Name;
            move.DoInclude = dto.DoInclude;
            move.ResourceName = dto.ResourceName;
            move.DamageClass = damageClass;
            move.ElementalType = type;
            move.AttackPower = dto.AttackPower;
            move.Accuracy = dto.Accuracy;
            move.PowerPoints = dto.PowerPoints;
            move.Priority = dto.Priority;
            move.PokeApiName = dto.PokeApiName;
            move.Effect = dto.Effect;

            return move;
        }
    }
}
