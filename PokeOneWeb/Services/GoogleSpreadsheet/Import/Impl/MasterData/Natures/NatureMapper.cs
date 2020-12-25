using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Natures
{
    public class NatureMapper : ISpreadsheetEntityMapper<NatureDto, Nature>
    {
        private readonly ILogger<NatureMapper> _logger;

        public NatureMapper(ILogger<NatureMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Nature> Map(IEnumerable<NatureDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Nature DTO. Skipping.");
                    continue;
                }

                yield return new Nature
                {
                    Name = dto.Name,
                    StatBoost = new Stats
                    {
                        Attack = dto.Attack,
                        SpecialAttack = dto.SpecialAttack,
                        Defense = dto.Defense,
                        SpecialDefense = dto.SpecialDefense,
                        Speed = dto.Speed,
                        HitPoints = 0
                    }
                };
            }
        }

        private bool IsValid(NatureDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }
    }
}
