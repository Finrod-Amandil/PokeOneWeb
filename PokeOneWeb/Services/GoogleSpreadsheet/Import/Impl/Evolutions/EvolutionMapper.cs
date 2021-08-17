using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Evolutions
{
    public class EvolutionMapper : ISpreadsheetEntityMapper<EvolutionSheetDto, Evolution>
    {
        private readonly ILogger<EvolutionSheetDto> _logger;

        private IDictionary<string, PokemonVariety> _varieties;
        private IDictionary<string, PokemonSpecies> _species;

        public EvolutionMapper(ILogger<EvolutionSheetDto> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Evolution> Map(IDictionary<RowHash, EvolutionSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _varieties = new Dictionary<string, PokemonVariety>();
            _species = new Dictionary<string, PokemonSpecies>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Evolution DTO. Skipping.");
                    continue;
                }

                yield return MapEvolution(dto, rowHash);
            }
        }

        public IEnumerable<Evolution> MapOnto(IList<Evolution> entities, IDictionary<RowHash, EvolutionSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _varieties = new Dictionary<string, PokemonVariety>();
            _species = new Dictionary<string, PokemonSpecies>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Evolution DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching Evolution entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapEvolution(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private bool IsValid(EvolutionSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.BasePokemonSpeciesName) &&
                !string.IsNullOrWhiteSpace(dto.BasePokemonVarietyName) &&
                !string.IsNullOrWhiteSpace(dto.EvolvedPokemonVarietyName);
        }

        private Evolution MapEvolution(EvolutionSheetDto dto, RowHash rowHash, Evolution evolution = null)
        {
            evolution ??= new Evolution();

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

            evolution.IdHash = rowHash.IdHash;
            evolution.Hash = rowHash.ContentHash;
            evolution.ImportSheetId = rowHash.ImportSheetId;
            evolution.BasePokemonSpecies = basePokemonSpecies;
            evolution.BasePokemonVariety = baseVariety;
            evolution.EvolvedPokemonVariety = evolvedVariety;
            evolution.EvolutionTrigger = dto.EvolutionTrigger;
            evolution.BaseStage = dto.BaseStage;
            evolution.EvolvedStage = dto.EvolvedStage;
            evolution.IsReversible = dto.IsReversible;
            evolution.IsAvailable = dto.IsAvailable;
            evolution.DoInclude = dto.DoInclude;

            return evolution;
        }
    }
}
