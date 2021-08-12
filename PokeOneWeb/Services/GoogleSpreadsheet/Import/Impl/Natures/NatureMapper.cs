using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Natures
{
    public class NatureMapper : ISpreadsheetEntityMapper<NatureDto, Nature>
    {
        private readonly ILogger<NatureMapper> _logger;

        public NatureMapper(ILogger<NatureMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Nature> Map(IDictionary<RowHash, NatureDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning("Found invalid Nature DTO. Skipping.");
                    continue;
                }

                yield return MapNature(dto, rowHash);
            }
        }

        public IEnumerable<Nature> MapOnto(IList<Nature> entities, IDictionary<RowHash, NatureDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Nature DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching Nature entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapNature(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private bool IsValid(NatureDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        private Nature MapNature(NatureDto dto, RowHash rowHash, Nature nature = null)
        {
            nature ??= new Nature();

            nature.IdHash = rowHash.IdHash;
            nature.Hash = rowHash.ContentHash;
            nature.ImportSheetId = rowHash.ImportSheetId;
            nature.Name = dto.Name;
            nature.Attack = dto.Attack;
            nature.SpecialAttack = dto.SpecialAttack;
            nature.Defense = dto.Defense;
            nature.SpecialDefense = dto.SpecialDefense;
            nature.Speed = dto.Speed;

            return nature;
        }
    }
}
