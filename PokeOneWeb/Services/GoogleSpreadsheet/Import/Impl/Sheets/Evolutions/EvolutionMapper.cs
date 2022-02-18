using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Evolutions
{
    public class EvolutionMapper : SpreadsheetEntityMapper<EvolutionSheetDto, Evolution>
    {
        private readonly Dictionary<string, PokemonVariety> _varieties = new();
        private readonly Dictionary<string, PokemonSpecies> _species = new();

        public EvolutionMapper(ISpreadsheetImportReporter reporter) : base(reporter) { }

        protected override Entity Entity => Entity.Evolution;

        protected override bool IsValid(EvolutionSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.BasePokemonSpeciesName) &&
                !string.IsNullOrWhiteSpace(dto.BasePokemonVarietyName) &&
                !string.IsNullOrWhiteSpace(dto.EvolvedPokemonVarietyName);
        }

        protected override string GetUniqueName(EvolutionSheetDto dto)
        {
            return dto.BasePokemonVarietyName + dto.EvolvedPokemonVarietyName;
        }

        protected override Evolution MapEntity(EvolutionSheetDto dto, RowHash rowHash, Evolution evolution = null)
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
