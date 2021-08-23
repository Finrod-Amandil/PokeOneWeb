using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Abilities
{
    public class AbilityMapper : ISpreadsheetEntityMapper<AbilitySheetDto, Ability>
    {
        private readonly ILogger<AbilityMapper> _logger;

        public AbilityMapper(ILogger<AbilityMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Ability> Map(IDictionary<RowHash, AbilitySheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning("Found invalid Ability DTO. Skipping.");
                    continue;
                }

                yield return MapAbility(dto, rowHash);
            }
        }

        public IEnumerable<Ability> MapOnto(IList<Ability> entities, IDictionary<RowHash, AbilitySheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Ability DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching Ability entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapAbility(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private static bool IsValid(AbilitySheetDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        private Ability MapAbility(AbilitySheetDto dto, RowHash rowHash, Ability ability = null)
        {
            ability ??= new Ability();

            ability.IdHash = rowHash.IdHash;
            ability.Hash = rowHash.ContentHash;
            ability.ImportSheetId = rowHash.ImportSheetId;
            ability.PokeApiName = dto.PokeApiName;
            ability.Name = dto.Name;
            ability.EffectDescription = dto.Effect;
            ability.EffectShortDescription = dto.ShortEffect;

            ability.AttackBoost = dto.AtkBoost;
            ability.SpecialAttackBoost = dto.SpaBoost;
            ability.DefenseBoost = dto.DefBoost;
            ability.SpecialDefenseBoost = dto.SpdBoost;
            ability.SpeedBoost = dto.SpeBoost;
            ability.HitPointsBoost = dto.HpBoost;
            ability.BoostConditions = dto.BoostConditions;

            return ability;
        }
    }
}
