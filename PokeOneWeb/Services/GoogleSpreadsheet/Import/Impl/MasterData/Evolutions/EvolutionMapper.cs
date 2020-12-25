using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Evolutions
{
    public class EvolutionMapper : ISpreadsheetEntityMapper<EvolutionDto, Evolution>
    {
        private readonly ILogger<EvolutionDto> _logger;

        private IDictionary<string, PokemonVariety> _varieties;
        private IDictionary<string, PokemonSpecies> _species;

        public EvolutionMapper(ILogger<EvolutionDto> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Evolution> Map(IEnumerable<EvolutionDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            _varieties = new Dictionary<string, PokemonVariety>();
            _species = new Dictionary<string, PokemonSpecies>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Evolution DTO. Skipping.");
                    continue;
                }

                PokemonSpecies basePokemonSpecies;
                if (_species.ContainsKey(dto.BasePokemonSpeciesName))
                {
                    basePokemonSpecies = _species[dto.BasePokemonSpeciesName];
                }
                else
                {
                    basePokemonSpecies = new PokemonSpecies { Name = dto.BasePokemonSpeciesName };
                    _species.Add(dto.BasePokemonSpeciesName, basePokemonSpecies);
                }

                PokemonVariety baseVariety;
                if (_varieties.ContainsKey(dto.BasePokemonVarietyName))
                {
                    baseVariety = _varieties[dto.BasePokemonVarietyName];
                }
                else
                {
                    baseVariety = new PokemonVariety { Name = dto.BasePokemonVarietyName };
                    _varieties.Add(dto.BasePokemonVarietyName, baseVariety);
                }

                PokemonVariety evolvedVariety;
                if (_varieties.ContainsKey(dto.EvolvedPokemonVarietyName))
                {
                    evolvedVariety = _varieties[dto.EvolvedPokemonVarietyName];
                }
                else
                {
                    evolvedVariety = new PokemonVariety { Name = dto.EvolvedPokemonVarietyName };
                    _varieties.Add(dto.EvolvedPokemonVarietyName, evolvedVariety);
                }

                yield return new Evolution
                {
                    BasePokemonSpecies = basePokemonSpecies,
                    BasePokemonVariety = baseVariety,
                    EvolvedPokemonVariety = evolvedVariety,
                    EvolutionTrigger = dto.EvolutionTrigger,
                    BaseStage = dto.BaseStage,
                    EvolvedStage = dto.EvolvedStage,
                    IsReversible = dto.IsReversible,
                    IsAvailable = dto.IsAvailable,
                    DoInclude = dto.DoInclude
                };
            }
        }

        private bool IsValid(EvolutionDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.BasePokemonSpeciesName) &&
                !string.IsNullOrWhiteSpace(dto.BasePokemonVarietyName) &&
                !string.IsNullOrWhiteSpace(dto.EvolvedPokemonVarietyName);
        }
    }
}
