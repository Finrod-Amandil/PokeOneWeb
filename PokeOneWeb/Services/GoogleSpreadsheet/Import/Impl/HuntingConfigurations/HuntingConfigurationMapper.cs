using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.HuntingConfigurations
{
    public class HuntingConfigurationMapper : ISpreadsheetEntityMapper<HuntingConfigurationSheetDto, HuntingConfiguration>
    {
        private readonly ILogger<HuntingConfigurationMapper> _logger;

        private IDictionary<string, PokemonVariety> _pokemonVarieties;
        private IDictionary<string, Nature> _natures;
        private IDictionary<string, Ability> _abilities;

        public HuntingConfigurationMapper(ILogger<HuntingConfigurationMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<HuntingConfiguration> Map(IDictionary<RowHash, HuntingConfigurationSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _pokemonVarieties = new Dictionary<string, PokemonVariety>();
            _natures = new Dictionary<string, Nature>();
            _abilities = new Dictionary<string, Ability>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid HuntingConfiguration DTO. Skipping.");
                    continue;
                }

                yield return MapHuntingConfiguration(dto, rowHash);
            }
        }

        public IEnumerable<HuntingConfiguration> MapOnto(IList<HuntingConfiguration> entities, IDictionary<RowHash, HuntingConfigurationSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _pokemonVarieties = new Dictionary<string, PokemonVariety>();
            _natures = new Dictionary<string, Nature>();
            _abilities = new Dictionary<string, Ability>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid HuntingConfiguration DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching HuntingConfiguration entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapHuntingConfiguration(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        public IEnumerable<HuntingConfiguration> Map(IEnumerable<HuntingConfigurationSheetDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            _pokemonVarieties = new Dictionary<string, PokemonVariety>();
            _natures = new Dictionary<string, Nature>();
            _abilities = new Dictionary<string, Ability>();

            foreach (var dto in dtos)
            {
                var pokemonVariety = MapPokemonVariety(dto);
                var nature = MapNature(dto);
                var ability = MapAbility(dto);

                yield return new HuntingConfiguration
                {
                    PokemonVariety = pokemonVariety,
                    Nature = nature,
                    Ability = ability
                };
            }
        }

        private static bool IsValid(HuntingConfigurationSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.Ability) &&
                !string.IsNullOrWhiteSpace(dto.Nature) &&
                !string.IsNullOrWhiteSpace(dto.PokemonVarietyName);
        }

        private HuntingConfiguration MapHuntingConfiguration(
            HuntingConfigurationSheetDto dto,
            RowHash rowHash,
            HuntingConfiguration huntingConfiguration = null)
        {
            huntingConfiguration ??= new HuntingConfiguration();

            huntingConfiguration.IdHash = rowHash.IdHash;
            huntingConfiguration.Hash = rowHash.ContentHash;
            huntingConfiguration.ImportSheetId = rowHash.ImportSheetId;
            huntingConfiguration.PokemonVariety = MapPokemonVariety(dto);
            huntingConfiguration.Ability = MapAbility(dto);
            huntingConfiguration.Nature = MapNature(dto);

            return huntingConfiguration;
        }

        private PokemonVariety MapPokemonVariety(HuntingConfigurationSheetDto dto)
        {
            PokemonVariety pokemonVariety;
            if (!_pokemonVarieties.ContainsKey(dto.PokemonVarietyName))
            {
                pokemonVariety = new PokemonVariety { Name = dto.PokemonVarietyName };
                _pokemonVarieties.Add(dto.PokemonVarietyName, pokemonVariety);
            }
            else
            {
                pokemonVariety = _pokemonVarieties[dto.PokemonVarietyName];
            }

            return pokemonVariety;
        }

        private Nature MapNature(HuntingConfigurationSheetDto dto)
        {
            Nature nature;
            if (!_natures.ContainsKey(dto.Nature))
            {
                nature = new Nature { Name = dto.Nature };
                _natures.Add(dto.Nature, nature);
            }
            else
            {
                nature = _natures[dto.Nature];
            }

            return nature;
        }

        private Ability MapAbility(HuntingConfigurationSheetDto dto)
        {
            Ability ability;
            if (!_abilities.ContainsKey(dto.Ability))
            {
                ability = new Ability { Name = dto.Ability };
                _abilities.Add(dto.Ability, ability);
            }
            else
            {
                ability = _abilities[dto.Ability];
            }

            return ability;
        }
    }
}
