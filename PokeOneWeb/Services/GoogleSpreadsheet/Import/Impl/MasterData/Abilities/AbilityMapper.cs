using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Abilities
{
    public class AbilityMapper : ISpreadsheetEntityMapper<AbilityDto, Ability>
    {
        private readonly ILogger<AbilityMapper> _logger;

        public AbilityMapper(ILogger<AbilityMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Ability> Map(IEnumerable<AbilityDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Ability DTO. Skipping.");
                    continue;
                }

                yield return new Ability
                {
                    PokeApiName = dto.PokeApiName,
                    Name = dto.Name,
                    EffectDescription = dto.Effect,
                    EffectShortDescription = dto.ShortEffect
                };
            }
        }

        private bool IsValid(AbilityDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }
    }
}
