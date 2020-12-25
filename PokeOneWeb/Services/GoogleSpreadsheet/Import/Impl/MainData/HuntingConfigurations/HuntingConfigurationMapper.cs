using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.HuntingConfigurations
{
    public class HuntingConfigurationMapper : ISpreadsheetEntityMapper<HuntingConfigurationDto, HuntingConfiguration>
    {
        private readonly ILogger<HuntingConfigurationMapper> _logger;

        private IDictionary<string, PokemonVariety> _pokemonVarieties;
        private IDictionary<string, Nature> _natures;
        private IDictionary<string, Ability> _abilities;

        public HuntingConfigurationMapper(ILogger<HuntingConfigurationMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<HuntingConfiguration> Map(IEnumerable<HuntingConfigurationDto> dtos)
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

        private PokemonVariety MapPokemonVariety(HuntingConfigurationDto dto)
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

        private Nature MapNature(HuntingConfigurationDto dto)
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

        private Ability MapAbility(HuntingConfigurationDto dto)
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
