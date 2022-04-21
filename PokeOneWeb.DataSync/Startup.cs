using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Configuration;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Abilities;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Availabilities;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.BagCategories;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Builds;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Currencies;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.ElementalTypeRelations;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.ElementalTypes;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Events;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Evolutions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.HuntingConfigurations;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Items;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.ItemStatBoosts;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.LearnableMoveLearnMethods;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Locations;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveDamageClasses;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveLearnMethodLocations;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Moves;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveTutorMoves;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveTutors;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Natures;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Pokemon;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.PvpTiers;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Regions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Seasons;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.SeasonTimesOfDay;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Spawns;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.SpawnTypes;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.TimesOfDay;
using PokeOneWeb.DataSync.ReadModelUpdate;
using PokeOneWeb.DataSync.ReadModelUpdate.Impl;
using PokeOneWeb.DataSync.ReadModelUpdate.Impl.EntityTypes;
using PokeOneWeb.DataSync.ReadModelUpdate.Impl.Item;
using PokeOneWeb.DataSync.ReadModelUpdate.Impl.ItemStatBoostPokemon;
using PokeOneWeb.DataSync.ReadModelUpdate.Impl.LearnableMoves;
using PokeOneWeb.DataSync.ReadModelUpdate.Impl.LocationGroups;
using PokeOneWeb.DataSync.ReadModelUpdate.Impl.Moves;
using PokeOneWeb.DataSync.ReadModelUpdate.Impl.Natures;
using PokeOneWeb.DataSync.ReadModelUpdate.Impl.Pokemon;
using PokeOneWeb.DataSync.ReadModelUpdate.Impl.Region;

namespace PokeOneWeb.DataSync
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                options.ConfigureWarnings(w => w.Throw(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
            });

            services.AddDbContext<ReadModelDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("ReadModelConnection"),
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                options.ConfigureWarnings(w => w.Throw(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
            });

            services.Configure<GoogleSpreadsheetsSettings>(options =>
                configuration.GetSection("GoogleSpreadsheetsSettings").Bind(options));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<IGoogleSpreadsheetImportService, GoogleSpreadsheetImportService>();
            services.AddScoped<IHashListComparator, HashListComparator>();
            services.AddScoped<ISheetNameHelper, SheetNameHelper>();
            services.AddScoped<ISpreadsheetDataLoader, SpreadsheetDataLoader>();
            services.AddSingleton<ISpreadsheetImportReporter, SpreadsheetImportReporter>();

            services.AddScoped<XISheetRowParser<AbilitySheetDto>, AbilityXSheetRowParser>();
            services.AddScoped<XISheetRowParser<AvailabilitySheetDto>, AvailabilityXSheetRowParser>();
            services.AddScoped<XISheetRowParser<BagCategorySheetDto>, BagCategoryXSheetRowParser>();
            services.AddScoped<XISheetRowParser<BuildSheetDto>, BuildXSheetRowParser>();
            services.AddScoped<XISheetRowParser<CurrencySheetDto>, CurrencyXSheetRowParser>();
            services.AddScoped<XISheetRowParser<ElementalTypeRelationSheetDto>, ElementalTypeRelationXSheetRowParser>();
            services.AddScoped<XISheetRowParser<ElementalTypeSheetDto>, ElementalTypeXSheetRowParser>();
            services.AddScoped<XISheetRowParser<EventSheetDto>, EventXSheetRowParser>();
            services.AddScoped<XISheetRowParser<EvolutionSheetDto>, EvolutionXSheetRowParser>();
            services.AddScoped<XISheetRowParser<HuntingConfigurationSheetDto>, HuntingConfigurationXSheetRowParser>();
            services.AddScoped<XISheetRowParser<ItemSheetDto>, ItemXSheetRowParser>();
            services.AddScoped<XISheetRowParser<ItemStatBoostSheetDto>, ItemStatBoostXSheetRowParser>();
            services.AddScoped<XISheetRowParser<LearnableMoveLearnMethodSheetDto>, LearnableMoveLearnMethodXSheetRowParser>();
            services.AddScoped<XISheetRowParser<LocationSheetDto>, LocationXSheetRowParser>();
            services.AddScoped<XISheetRowParser<MoveDamageClassSheetDto>, MoveDamageClassXSheetRowParser>();
            services.AddScoped<XISheetRowParser<MoveLearnMethodLocationSheetDto>, MoveLearnMethodLocationXSheetRowParser>();
            services.AddScoped<XISheetRowParser<MoveSheetDto>, MoveXSheetRowParser>();
            services.AddScoped<XISheetRowParser<MoveTutorMoveSheetDto>, MoveTutorMoveXSheetRowParser>();
            services.AddScoped<XISheetRowParser<MoveTutorSheetDto>, MoveTutorXSheetRowParser>();
            services.AddScoped<XISheetRowParser<NatureSheetDto>, NatureXSheetRowParser>();
            services.AddScoped<XISheetRowParser<PlacedItemSheetDto>, PlacedItemSheetRowParser>();
            services.AddScoped<XISheetRowParser<PokemonSheetDto>, PokemonXSheetRowParser>();
            services.AddScoped<XISheetRowParser<PvpTierSheetDto>, PvpTierXSheetRowParser>();
            services.AddScoped<XISheetRowParser<RegionSheetDto>, RegionXSheetRowParser>();
            services.AddScoped<XISheetRowParser<SeasonSheetDto>, SeasonXSheetRowParser>();
            services.AddScoped<XISheetRowParser<SeasonTimeOfDaySheetDto>, SeasonTimeOfDayXSheetRowParser>();
            services.AddScoped<XISheetRowParser<SpawnSheetDto>, SpawnXSheetRowParser>();
            services.AddScoped<XISheetRowParser<SpawnTypeSheetDto>, SpawnTypeXSheetRowParser>();
            services.AddScoped<XISheetRowParser<TimeOfDaySheetDto>, TimeOfDayXSheetRowParser>();

            services.AddScoped<XISpreadsheetEntityMapper<AbilitySheetDto, Ability>, AbilityMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<AvailabilitySheetDto, PokemonAvailability>, AvailabilityMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<BagCategorySheetDto, BagCategory>, BagCategoryMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<BuildSheetDto, Build>, BuildMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<CurrencySheetDto, Currency>, CurrencyMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<ElementalTypeRelationSheetDto, ElementalTypeRelation>, ElementalTypeRelationMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<ElementalTypeSheetDto, ElementalType>, ElementalTypeMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<EventSheetDto, Event>, EventMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<EvolutionSheetDto, Evolution>, EvolutionMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<HuntingConfigurationSheetDto, HuntingConfiguration>, HuntingConfigurationMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<ItemSheetDto, Item>, ItemMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<ItemStatBoostSheetDto, ItemStatBoostPokemon>, ItemStatBoostMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<LearnableMoveLearnMethodSheetDto, LearnableMoveLearnMethod>, LearnableMoveLearnMethodMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<LocationSheetDto, Location>, LocationMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<MoveDamageClassSheetDto, MoveDamageClass>, MoveDamageClassMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<MoveLearnMethodLocationSheetDto, MoveLearnMethodLocation>, MoveLearnMethodLocationMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<MoveSheetDto, Move>, MoveMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<MoveTutorMoveSheetDto, MoveTutorMove>, MoveTutorMoveMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<MoveTutorSheetDto, MoveTutor>, MoveTutorMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<NatureSheetDto, Nature>, NatureMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<PlacedItemSheetDto, PlacedItem>, PlacedItemMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<PokemonSheetDto, PokemonForm>, PokemonMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<PvpTierSheetDto, PvpTier>, PvpTierMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<RegionSheetDto, Region>, RegionMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<SeasonSheetDto, Season>, SeasonMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<SeasonTimeOfDaySheetDto, SeasonTimeOfDay>, SeasonTimeOfDayMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<SpawnSheetDto, Spawn>, SpawnMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<SpawnTypeSheetDto, SpawnType>, SpawnTypeMapper>();
            services.AddScoped<XISpreadsheetEntityMapper<TimeOfDaySheetDto, TimeOfDay>, TimeOfDayMapper>();

            services.AddScoped<AbilityXiSheetRepository>();
            services.AddScoped<AvailabilityXiSheetRepository>();
            services.AddScoped<BagCategoryXiSheetRepository>();
            services.AddScoped<BuildXiSheetRepository>();
            services.AddScoped<CurrencyXiSheetRepository>();
            services.AddScoped<ElementalTypeRelationXiSheetRepository>();
            services.AddScoped<ElementalTypeXiSheetRepository>();
            services.AddScoped<EventXiSheetRepository>();
            services.AddScoped<EvolutionXiSheetRepository>();
            services.AddScoped<HuntingConfigurationXiSheetRepository>();
            services.AddScoped<ItemXiSheetRepository>();
            services.AddScoped<ItemStatBoostXiSheetRepository>();
            services.AddScoped<LearnableMoveLearnMethodXiSheetRepository>();
            services.AddScoped<LocationXiSheetRepository>();
            services.AddScoped<MoveDamageClassXiSheetRepository>();
            services.AddScoped<MoveLearnMethodLocationXiSheetRepository>();
            services.AddScoped<MoveXiSheetRepository>();
            services.AddScoped<MoveTutorMoveXiSheetRepository>();
            services.AddScoped<MoveTutorXiSheetRepository>();
            services.AddScoped<NatureXiSheetRepository>();
            services.AddScoped<PlacedItemSheetRepository>();
            services.AddScoped<PokemonXiSheetRepository>();
            services.AddScoped<PvpTierXiSheetRepository>();
            services.AddScoped<RegionXiSheetRepository>();
            services.AddScoped<SeasonXiSheetRepository>();
            services.AddScoped<SeasonTimeOfDayXiSheetRepository>();
            services.AddScoped<SpawnXiSheetRepository>();
            services.AddScoped<SpawnTypeXiSheetRepository>();
            services.AddScoped<TimeOfDayXiSheetRepository>();

            services.AddScoped<IReadModelUpdateService, ReadModelUpdateService>();

            services.AddScoped<IReadModelMapper<EntityTypeReadModel>, EntityTypeReadModelMapper>();
            services.AddScoped<IReadModelMapper<ItemStatBoostPokemonReadModel>, ItemStatBoostPokemonReadModelMapper>();
            services.AddScoped<IReadModelMapper<SimpleLearnableMoveReadModel>, SimpleLearnableMoveReadModelMapper>();
            services.AddScoped<IReadModelMapper<MoveReadModel>, MoveReadModelMapper>();
            services.AddScoped<IReadModelMapper<NatureReadModel>, NatureReadModelMapper>();
            services.AddScoped<IReadModelMapper<PokemonVarietyReadModel>, PokemonReadModelMapper>();
            services.AddScoped<IReadModelMapper<ItemReadModel>, ItemReadModelMapper>();
            services.AddScoped<IReadModelMapper<RegionReadModel>, RegionReadModelMapper>();
            services.AddScoped<IReadModelMapper<LocationGroupReadModel>, LocationGroupReadModelMapper>();

            services.AddScoped<IReadModelRepository<EntityTypeReadModel>, EntityTypeReadModelRepository>();
            services.AddScoped<IReadModelRepository<ItemStatBoostPokemonReadModel>, ItemStatBoostPokemonReadModelRepository>();
            services.AddScoped<IReadModelRepository<SimpleLearnableMoveReadModel>, SimpleLearnableMoveReadModelRepository>();
            services.AddScoped<IReadModelRepository<MoveReadModel>, MoveReadModelRepository>();
            services.AddScoped<IReadModelRepository<NatureReadModel>, NatureReadModelRepository>();
            services.AddScoped<IReadModelRepository<PokemonVarietyReadModel>, PokemonVarietyReadModelRepository>();
            services.AddScoped<IReadModelRepository<ItemReadModel>, ItemReadModelRepository>();
            services.AddScoped<IReadModelRepository<RegionReadModel>, RegionReadModelRepository>();
            services.AddScoped<IReadModelRepository<LocationGroupReadModel>, LocationGroupReadModelRepository>();
        }
    }
}