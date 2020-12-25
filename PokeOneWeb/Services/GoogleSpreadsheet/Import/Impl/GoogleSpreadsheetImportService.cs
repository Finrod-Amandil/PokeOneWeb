using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.Builds;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.HuntingConfigurations;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.LearnableMoveLearnMethods;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.Locations;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.PlacedItems;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.Regions;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.Spawns;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.TutorMoves;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Abilities;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Availabilities;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Currencies;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.ElementalTypeRelations;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.ElementalTypes;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Evolutions;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Items;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Moves;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Natures;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Pokemon;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.PvpTiers;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.SpawnTypes;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Times;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl
{
    public class GoogleSpreadsheetImportService : IGoogleSpreadsheetImportService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ISpreadsheetLoader _spreadsheetLoader;

        private readonly ISpreadsheetEntityImporter<ItemDto, Item> _itemImporter;
        private readonly ISpreadsheetEntityImporter<AbilityDto, Ability> _abilityImporter;
        private readonly ISpreadsheetEntityImporter<ElementalTypeDto, ElementalType> _elementalTypeImporter;
        private readonly ISpreadsheetEntityImporter<ElementalTypeRelationDto, ElementalTypeRelation> _elementalTypeRelationImporter;
        private readonly ISpreadsheetEntityImporter<MoveDto, Move> _moveImporter;
        private readonly ISpreadsheetEntityImporter<NatureDto, Nature> _natureImporter;
        private readonly ISpreadsheetEntityImporter<TimeDto, SeasonTimeOfDay> _timeImporter;
        private readonly ISpreadsheetEntityImporter<CurrencyDto, Currency> _currencyImporter;
        private readonly ISpreadsheetEntityImporter<AvailabilityDto, PokemonAvailability> _availabilityImporter;
        private readonly ISpreadsheetEntityImporter<PvpTierDto, PvpTier> _pvpTierImporter;
        private readonly ISpreadsheetEntityImporter<PokemonDto, PokemonForm> _pokemonImporter;
        private readonly ISpreadsheetEntityImporter<EvolutionDto, Evolution> _evolutionImporter;
        private readonly ISpreadsheetEntityImporter<SpawnTypeDto, SpawnType> _spawnTypeImporter;

        private readonly ISpreadsheetEntityImporter<RegionDto, Region> _regionImporter;
        private readonly ISpreadsheetEntityImporter<LocationDto, Location> _locationImporter;
        private readonly ISpreadsheetEntityImporter<SpawnDto, Spawn> _spawnImporter;
        private readonly ISpreadsheetEntityImporter<PlacedItemDto, PlacedItem> _placedItemImporter;
        private readonly ISpreadsheetEntityImporter<TutorMoveDto, TutorMove> _tutorMoveImporter;
        private readonly ISpreadsheetEntityImporter<LearnableMoveLearnMethodDto, LearnableMove> _learnableMoveImporter;
        private readonly ISpreadsheetEntityImporter<BuildDto, Build> _buildImporter;
        private readonly ISpreadsheetEntityImporter<HuntingConfigurationDto, HuntingConfiguration> _huntingConfigurationImporter;

        public GoogleSpreadsheetImportService(
            ApplicationDbContext dbContext,
            ISpreadsheetLoader spreadsheetLoader,

            ISpreadsheetEntityImporter<ItemDto, Item> itemImporter,
            ISpreadsheetEntityImporter<AbilityDto, Ability> abilityImporter,
            ISpreadsheetEntityImporter<ElementalTypeDto, ElementalType> elementalTypeImporter,
            ISpreadsheetEntityImporter<ElementalTypeRelationDto, ElementalTypeRelation> elementalTypeRelationImporter,
            ISpreadsheetEntityImporter<MoveDto, Move> moveImporter,
            ISpreadsheetEntityImporter<NatureDto, Nature> natureImporter,
            ISpreadsheetEntityImporter<TimeDto, SeasonTimeOfDay> timeImporter,
            ISpreadsheetEntityImporter<CurrencyDto, Currency> currencyImporter,
            ISpreadsheetEntityImporter<AvailabilityDto, PokemonAvailability> availabilityImporter,
            ISpreadsheetEntityImporter<PvpTierDto, PvpTier> pvpTierImporter,
            ISpreadsheetEntityImporter<PokemonDto, PokemonForm> pokemonImporter,
            ISpreadsheetEntityImporter<EvolutionDto, Evolution> evolutionImporter,
            ISpreadsheetEntityImporter<SpawnTypeDto, SpawnType> spawnTypeImporter,

            ISpreadsheetEntityImporter<RegionDto, Region> regionImporter,
            ISpreadsheetEntityImporter<LocationDto, Location> locationImporter,
            ISpreadsheetEntityImporter<SpawnDto, Spawn> spawnImporter,
            ISpreadsheetEntityImporter<PlacedItemDto, PlacedItem> placedItemImporter,
            ISpreadsheetEntityImporter<TutorMoveDto, TutorMove> tutorMoveImporter,
            ISpreadsheetEntityImporter<LearnableMoveLearnMethodDto, LearnableMove> learnableMoveImporter,
            ISpreadsheetEntityImporter<BuildDto, Build> buildImporter,
            ISpreadsheetEntityImporter<HuntingConfigurationDto, HuntingConfiguration> huntingConfigurationImporter)
        {
            _dbContext = dbContext;
            _spreadsheetLoader = spreadsheetLoader;
            _itemImporter = itemImporter;
            _abilityImporter = abilityImporter;
            _elementalTypeImporter = elementalTypeImporter;
            _elementalTypeRelationImporter = elementalTypeRelationImporter;
            _moveImporter = moveImporter;
            _natureImporter = natureImporter;
            _timeImporter = timeImporter;
            _currencyImporter = currencyImporter;
            _availabilityImporter = availabilityImporter;
            _pvpTierImporter = pvpTierImporter;
            _pokemonImporter = pokemonImporter;
            _evolutionImporter = evolutionImporter;
            _spawnTypeImporter = spawnTypeImporter;
            _regionImporter = regionImporter;
            _locationImporter = locationImporter;
            _spawnImporter = spawnImporter;
            _placedItemImporter = placedItemImporter;
            _tutorMoveImporter = tutorMoveImporter;
            _learnableMoveImporter = learnableMoveImporter;
            _buildImporter = buildImporter;
            _huntingConfigurationImporter = huntingConfigurationImporter;
        }

        public async Task ImportSpreadsheetData()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.Database.MigrateAsync();

            //Master data
            var masterDataSpreadsheet = await _spreadsheetLoader.LoadSpreadsheet(Constants.MASTER_SPREADSHEET_ID);
            _itemImporter.ImportFromSpreadsheet(masterDataSpreadsheet);
            _abilityImporter.ImportFromSpreadsheet(masterDataSpreadsheet);
            _elementalTypeImporter.ImportFromSpreadsheet(masterDataSpreadsheet);
            _elementalTypeRelationImporter.ImportFromSpreadsheet(masterDataSpreadsheet);
            _moveImporter.ImportFromSpreadsheet(masterDataSpreadsheet);
            _natureImporter.ImportFromSpreadsheet(masterDataSpreadsheet);
            _timeImporter.ImportFromSpreadsheet(masterDataSpreadsheet);
            _currencyImporter.ImportFromSpreadsheet(masterDataSpreadsheet);
            _availabilityImporter.ImportFromSpreadsheet(masterDataSpreadsheet);
            _pvpTierImporter.ImportFromSpreadsheet(masterDataSpreadsheet);
            _pokemonImporter.ImportFromSpreadsheet(masterDataSpreadsheet);
            _evolutionImporter.ImportFromSpreadsheet(masterDataSpreadsheet);
            _spawnTypeImporter.ImportFromSpreadsheet(masterDataSpreadsheet);

            masterDataSpreadsheet = null;
            GC.Collect();

            //Main data
            var mainDataSpreadsheet = await _spreadsheetLoader.LoadSpreadsheet(Constants.MAIN_SPREADSHEET_ID);
            _regionImporter.ImportFromSpreadsheet(mainDataSpreadsheet);
            _locationImporter.ImportFromSpreadsheet(mainDataSpreadsheet);
            _spawnImporter.ImportFromSpreadsheet(mainDataSpreadsheet);
            _placedItemImporter.ImportFromSpreadsheet(mainDataSpreadsheet);
            _tutorMoveImporter.ImportFromSpreadsheet(mainDataSpreadsheet);
            _buildImporter.ImportFromSpreadsheet(mainDataSpreadsheet);
            _huntingConfigurationImporter.ImportFromSpreadsheet(mainDataSpreadsheet);

            mainDataSpreadsheet = null;
            GC.Collect();

            foreach (var learnableMovesSpreadsheetId in Constants.LEARNABLE_MOVES_SPREADSHEET_IDS)
            {
                var learnableMovesSpreadsheet = await _spreadsheetLoader.LoadSpreadsheet(learnableMovesSpreadsheetId);
                _learnableMoveImporter.ImportFromSpreadsheet(learnableMovesSpreadsheet);

                learnableMovesSpreadsheet = null;
                GC.Collect();
            }
        }
    }
}
