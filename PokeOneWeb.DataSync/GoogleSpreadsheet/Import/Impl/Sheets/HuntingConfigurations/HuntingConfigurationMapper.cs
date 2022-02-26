using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.HuntingConfigurations
{
    public class HuntingConfigurationMapper : SpreadsheetEntityMapper<HuntingConfigurationSheetDto, HuntingConfiguration>
    {
        private readonly Dictionary<string, PokemonVariety> _pokemonVarieties = new();
        private readonly Dictionary<string, Nature> _natures = new();
        private readonly Dictionary<string, Ability> _abilities = new();

        public HuntingConfigurationMapper(ISpreadsheetImportReporter reporter) : base(reporter) { }

        protected override Entity Entity => Entity.HuntingConfiguration;

        protected override bool IsValid(HuntingConfigurationSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.Ability) &&
                !string.IsNullOrWhiteSpace(dto.Nature) &&
                !string.IsNullOrWhiteSpace(dto.PokemonVarietyName);
        }

        protected override string GetUniqueName(HuntingConfigurationSheetDto dto)
        {
            return dto.PokemonVarietyName + dto.Ability + dto.Nature;
        }

        protected override HuntingConfiguration MapEntity(
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
