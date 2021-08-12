using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MoveDamageClasses
{
    public class MoveDamageClassMapper : ISpreadsheetEntityMapper<MoveDamageClassDto, MoveDamageClass>
    {
        private readonly ILogger<MoveDamageClassMapper> _logger;

        public MoveDamageClassMapper(ILogger<MoveDamageClassMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<MoveDamageClass> Map(IDictionary<RowHash, MoveDamageClassDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning("Found invalid MoveDamageClass DTO. Skipping.");
                    continue;
                }

                yield return MapMoveDamageClass(dto, rowHash);
            }
        }

        public IEnumerable<MoveDamageClass> MapOnto(IList<MoveDamageClass> entities, IDictionary<RowHash, MoveDamageClassDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid MoveDamageClass DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching MoveDamageClass entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapMoveDamageClass(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private static bool IsValid(MoveDamageClassDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        private MoveDamageClass MapMoveDamageClass(
            MoveDamageClassDto dto, 
            RowHash rowHash, 
            MoveDamageClass moveDamageClass = null)
        {
            moveDamageClass ??= new MoveDamageClass();

            moveDamageClass.IdHash = rowHash.IdHash;
            moveDamageClass.Hash = rowHash.ContentHash;
            moveDamageClass.ImportSheetId = rowHash.ImportSheetId;
            moveDamageClass.Name = dto.Name;

            return moveDamageClass;
        }
    }
}
