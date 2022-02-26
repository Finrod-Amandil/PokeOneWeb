using System.Configuration;
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
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.PlacedItems;
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
using PokeOneWeb.DataSync.ReadModelUpdate.Impl.Moves;
using PokeOneWeb.DataSync.ReadModelUpdate.Impl.Natures;
using PokeOneWeb.DataSync.ReadModelUpdate.Impl.Pokemon;

namespace PokeOneWeb.DataSync
{
    public class Startup
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

            services.AddScoped<ISheetRowParser<AbilitySheetDto>, AbilitySheetRowParser>();
            services.AddScoped<ISheetRowParser<AvailabilitySheetDto>, AvailabilitySheetRowParser>();
            services.AddScoped<ISheetRowParser<BagCategorySheetDto>, BagCategorySheetRowParser>();
            services.AddScoped<ISheetRowParser<BuildSheetDto>, BuildSheetRowParser>();
            services.AddScoped<ISheetRowParser<CurrencySheetDto>, CurrencySheetRowParser>();
            services.AddScoped<ISheetRowParser<ElementalTypeRelationSheetDto>, ElementalTypeRelationSheetRowParser>();
            services.AddScoped<ISheetRowParser<ElementalTypeSheetDto>, ElementalTypeSheetRowParser>();
            services.AddScoped<ISheetRowParser<EventSheetDto>, EventSheetRowParser>();
            services.AddScoped<ISheetRowParser<EvolutionSheetDto>, EvolutionSheetRowParser>();
            services.AddScoped<ISheetRowParser<HuntingConfigurationSheetDto>, HuntingConfigurationSheetRowParser>();
            services.AddScoped<ISheetRowParser<ItemSheetDto>, ItemSheetRowParser>();
            services.AddScoped<ISheetRowParser<ItemStatBoostSheetDto>, ItemStatBoostSheetRowParser>();
            services.AddScoped<ISheetRowParser<LearnableMoveLearnMethodSheetDto>, LearnableMoveLearnMethodSheetRowParser>();
            services.AddScoped<ISheetRowParser<LocationSheetDto>, LocationSheetRowParser>();
            services.AddScoped<ISheetRowParser<MoveDamageClassSheetDto>, MoveDamageClassSheetRowParser>();
            services.AddScoped<ISheetRowParser<MoveLearnMethodLocationSheetDto>, MoveLearnMethodLocationSheetRowParser>();
            services.AddScoped<ISheetRowParser<MoveSheetDto>, MoveSheetRowParser>();
            services.AddScoped<ISheetRowParser<MoveTutorMoveSheetDto>, MoveTutorMoveSheetRowParser>();
            services.AddScoped<ISheetRowParser<MoveTutorSheetDto>, MoveTutorSheetRowParser>();
            services.AddScoped<ISheetRowParser<NatureSheetDto>, NatureSheetRowParser>();
            services.AddScoped<ISheetRowParser<PlacedItemSheetDto>, PlacedItemSheetRowParser>();
            services.AddScoped<ISheetRowParser<PokemonSheetDto>, PokemonSheetRowParser>();
            services.AddScoped<ISheetRowParser<PvpTierSheetDto>, PvpTierSheetRowParser>();
            services.AddScoped<ISheetRowParser<RegionSheetDto>, RegionSheetRowParser>();
            services.AddScoped<ISheetRowParser<SeasonSheetDto>, SeasonSheetRowParser>();
            services.AddScoped<ISheetRowParser<SeasonTimeOfDaySheetDto>, SeasonTimeOfDaySheetRowParser>();
            services.AddScoped<ISheetRowParser<SpawnSheetDto>, SpawnSheetRowParser>();
            services.AddScoped<ISheetRowParser<SpawnTypeSheetDto>, SpawnTypeSheetRowParser>();
            services.AddScoped<ISheetRowParser<TimeOfDaySheetDto>, TimeOfDaySheetRowParser>();

            services.AddScoped<ISpreadsheetEntityMapper<AbilitySheetDto, Ability>, AbilityMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<AvailabilitySheetDto, PokemonAvailability>, AvailabilityMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<BagCategorySheetDto, BagCategory>, BagCategoryMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<BuildSheetDto, Build>, BuildMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<CurrencySheetDto, Currency>, CurrencyMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<ElementalTypeRelationSheetDto, ElementalTypeRelation>, ElementalTypeRelationMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<ElementalTypeSheetDto, ElementalType>, ElementalTypeMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<EventSheetDto, Event>, EventMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<EvolutionSheetDto, Evolution>, EvolutionMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<HuntingConfigurationSheetDto, HuntingConfiguration>, HuntingConfigurationMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<ItemSheetDto, Item>, ItemMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<ItemStatBoostSheetDto, ItemStatBoostPokemon>, ItemStatBoostMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<LearnableMoveLearnMethodSheetDto, LearnableMoveLearnMethod>, LearnableMoveLearnMethodMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<LocationSheetDto, Location>, LocationMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<MoveDamageClassSheetDto, MoveDamageClass>, MoveDamageClassMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<MoveLearnMethodLocationSheetDto, MoveLearnMethodLocation>, MoveLearnMethodLocationMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<MoveSheetDto, Move>, MoveMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<MoveTutorMoveSheetDto, MoveTutorMove>, MoveTutorMoveMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<MoveTutorSheetDto, MoveTutor>, MoveTutorMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<NatureSheetDto, Nature>, NatureMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<PlacedItemSheetDto, PlacedItem>, PlacedItemMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<PokemonSheetDto, PokemonForm>, PokemonMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<PvpTierSheetDto, PvpTier>, PvpTierMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<RegionSheetDto, Region>, RegionMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<SeasonSheetDto, Season>, SeasonMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<SeasonTimeOfDaySheetDto, SeasonTimeOfDay>, SeasonTimeOfDayMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<SpawnSheetDto, Spawn>, SpawnMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<SpawnTypeSheetDto, SpawnType>, SpawnTypeMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<TimeOfDaySheetDto, TimeOfDay>, TimeOfDayMapper>();

            services.AddScoped<AbilitySheetRepository>();
            services.AddScoped<AvailabilitySheetRepository>();
            services.AddScoped<BagCategorySheetRepository>();
            services.AddScoped<BuildSheetRepository>();
            services.AddScoped<CurrencySheetRepository>();
            services.AddScoped<ElementalTypeRelationSheetRepository>();
            services.AddScoped<ElementalTypeSheetRepository>();
            services.AddScoped<EventSheetRepository>();
            services.AddScoped<EvolutionSheetRepository>();
            services.AddScoped<HuntingConfigurationSheetRepository>();
            services.AddScoped<ItemSheetRepository>();
            services.AddScoped<ItemStatBoostSheetRepository>();
            services.AddScoped<LearnableMoveLearnMethodSheetRepository>();
            services.AddScoped<LocationSheetRepository>();
            services.AddScoped<MoveDamageClassSheetRepository>();
            services.AddScoped<MoveLearnMethodLocationSheetRepository>();
            services.AddScoped<MoveSheetRepository>();
            services.AddScoped<MoveTutorMoveSheetRepository>();
            services.AddScoped<MoveTutorSheetRepository>();
            services.AddScoped<NatureSheetRepository>();
            services.AddScoped<PlacedItemSheetRepository>();
            services.AddScoped<PokemonSheetRepository>();
            services.AddScoped<PvpTierSheetRepository>();
            services.AddScoped<RegionSheetRepository>();
            services.AddScoped<SeasonSheetRepository>();
            services.AddScoped<SeasonTimeOfDaySheetRepository>();
            services.AddScoped<SpawnSheetRepository>();
            services.AddScoped<SpawnTypeSheetRepository>();
            services.AddScoped<TimeOfDaySheetRepository>();

            services.AddScoped<IReadModelUpdateService, ReadModelUpdateService>();

            services.AddScoped<IReadModelMapper<EntityTypeReadModel>, EntityTypeReadModelMapper>();
            services.AddScoped<IReadModelMapper<ItemStatBoostPokemonReadModel>, ItemStatBoostPokemonReadModelMapper>();
            services.AddScoped<IReadModelMapper<SimpleLearnableMoveReadModel>, SimpleLearnableMoveReadModelMapper>();
            services.AddScoped<IReadModelMapper<MoveReadModel>, MoveReadModelMapper>();
            services.AddScoped<IReadModelMapper<NatureReadModel>, NatureReadModelMapper>();
            services.AddScoped<IReadModelMapper<PokemonVarietyReadModel>, PokemonReadModelMapper>();
            services.AddScoped<IReadModelMapper<ItemReadModel>, ItemReadModelMapper>();

            services.AddScoped<IReadModelRepository<EntityTypeReadModel>, EntityTypeReadModelRepository>();
            services.AddScoped<IReadModelRepository<ItemStatBoostPokemonReadModel>, ItemStatBoostPokemonReadModelRepository>();
            services.AddScoped<IReadModelRepository<SimpleLearnableMoveReadModel>, SimpleLearnableMoveReadModelRepository>();
            services.AddScoped<IReadModelRepository<MoveReadModel>, MoveReadModelRepository>();
            services.AddScoped<IReadModelRepository<NatureReadModel>, NatureReadModelRepository>();
            services.AddScoped<IReadModelRepository<PokemonVarietyReadModel>, PokemonVarietyReadModelRepository>();
            services.AddScoped<IReadModelRepository<ItemReadModel>, ItemReadModelRepository>();
        }
    }
}
