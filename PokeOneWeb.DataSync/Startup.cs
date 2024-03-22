using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.Data.Repositories;
using PokeOneWeb.Data.Repositories.Impl.EntityRepositories;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Configuration;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers;
using PokeOneWeb.DataSync.ReadModelUpdate.Impl;
using PokeOneWeb.DataSync.ReadModelUpdate.Interfaces;
using PokeOneWeb.DataSync.ReadModelUpdate.ReadModelMappers;

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

            services.Configure<GoogleSpreadsheetsSettings>(options =>
                configuration.GetSection("GoogleSpreadsheetsSettings").Bind(options));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<IGoogleSpreadsheetImportService, GoogleSpreadsheetImportService>();
            services.AddScoped<IHashListComparator, HashListComparator>();
            services.AddScoped<ISpreadsheetDataLoader, SpreadsheetDataLoader>();
            services.AddSingleton<ISpreadsheetImportReporter, SpreadsheetImportReporter>();

            services.AddScoped(typeof(SheetImporter<Ability>));
            services.AddScoped(typeof(SheetImporter<PokemonAvailability>));
            services.AddScoped(typeof(SheetImporter<BagCategory>));
            services.AddScoped(typeof(SheetImporter<Build>));
            services.AddScoped(typeof(SheetImporter<Currency>));
            services.AddScoped(typeof(SheetImporter<ElementalTypeRelation>));
            services.AddScoped(typeof(SheetImporter<ElementalType>));
            services.AddScoped(typeof(SheetImporter<Event>));
            services.AddScoped(typeof(SheetImporter<Evolution>));
            services.AddScoped(typeof(SheetImporter<HuntingConfiguration>));
            services.AddScoped(typeof(SheetImporter<Item>));
            services.AddScoped(typeof(SheetImporter<ItemStatBoostPokemon>));
            services.AddScoped(typeof(SheetImporter<LearnableMoveLearnMethod>));
            services.AddScoped(typeof(SheetImporter<Location>));
            services.AddScoped(typeof(SheetImporter<MoveDamageClass>));
            services.AddScoped(typeof(SheetImporter<MoveLearnMethodLocation>));
            services.AddScoped(typeof(SheetImporter<Move>));
            services.AddScoped(typeof(SheetImporter<MoveTutorMove>));
            services.AddScoped(typeof(SheetImporter<MoveTutor>));
            services.AddScoped(typeof(SheetImporter<Nature>));
            services.AddScoped(typeof(SheetImporter<PlacedItem>));
            services.AddScoped(typeof(SheetImporter<PokemonForm>));
            services.AddScoped(typeof(SheetImporter<PvpTier>));
            services.AddScoped(typeof(SheetImporter<Region>));
            services.AddScoped(typeof(SheetImporter<Season>));
            services.AddScoped(typeof(SheetImporter<SeasonTimeOfDay>));
            services.AddScoped(typeof(SheetImporter<Spawn>));
            services.AddScoped(typeof(SheetImporter<SpawnType>));
            services.AddScoped(typeof(SheetImporter<TimeOfDay>));

            services.AddScoped<ISheetMapper<Ability>, AbilitySheetMapper>();
            services.AddScoped<ISheetMapper<PokemonAvailability>, AvailabilitySheetMapper>();
            services.AddScoped<ISheetMapper<BagCategory>, BagCategorySheetMapper>();
            services.AddScoped<ISheetMapper<Build>, BuildSheetMapper>();
            services.AddScoped<ISheetMapper<Currency>, CurrencySheetMapper>();
            services.AddScoped<ISheetMapper<ElementalTypeRelation>, ElementalTypeRelationSheetMapper>();
            services.AddScoped<ISheetMapper<ElementalType>, ElementalTypeSheetMapper>();
            services.AddScoped<ISheetMapper<Event>, EventSheetMapper>();
            services.AddScoped<ISheetMapper<Evolution>, EvolutionSheetMapper>();
            services.AddScoped<ISheetMapper<HuntingConfiguration>, HuntingConfigurationSheetMapper>();
            services.AddScoped<ISheetMapper<Item>, ItemSheetMapper>();
            services.AddScoped<ISheetMapper<ItemStatBoostPokemon>, ItemStatBoostPokemonSheetMapper>();
            services.AddScoped<ISheetMapper<LearnableMoveLearnMethod>, LearnableMoveLearnMethodSheetMapper>();
            services.AddScoped<ISheetMapper<Location>, LocationSheetMapper>();
            services.AddScoped<ISheetMapper<MoveDamageClass>, MoveDamageClassSheetMapper>();
            services.AddScoped<ISheetMapper<MoveLearnMethodLocation>, MoveLearnMethodLocationSheetMapper>();
            services.AddScoped<ISheetMapper<Move>, MoveSheetMapper>();
            services.AddScoped<ISheetMapper<MoveTutorMove>, MoveTutorMoveSheetMapper>();
            services.AddScoped<ISheetMapper<MoveTutor>, MoveTutorSheetMapper>();
            services.AddScoped<ISheetMapper<Nature>, NatureSheetMapper>();
            services.AddScoped<ISheetMapper<PlacedItem>, PlacedItemSheetMapper>();
            services.AddScoped<ISheetMapper<PokemonForm>, PokemonFormSheetMapper>();
            services.AddScoped<ISheetMapper<PvpTier>, PvpTierSheetMapper>();
            services.AddScoped<ISheetMapper<Region>, RegionSheetMapper>();
            services.AddScoped<ISheetMapper<Season>, SeasonSheetMapper>();
            services.AddScoped<ISheetMapper<SeasonTimeOfDay>, SeasonTimeOfDaySheetMapper>();
            services.AddScoped<ISheetMapper<Spawn>, SpawnSheetMapper>();
            services.AddScoped<ISheetMapper<SpawnType>, SpawnTypeSheetMapper>();
            services.AddScoped<ISheetMapper<TimeOfDay>, TimeOfDaySheetMapper>();

            services.AddScoped<IHashedEntityRepository<Ability>, AbilityRepository>();
            services.AddScoped<IHashedEntityRepository<PokemonAvailability>, AvailabilityRepository>();
            services.AddScoped<IHashedEntityRepository<BagCategory>, BagCategoryRepository>();
            services.AddScoped<IHashedEntityRepository<Build>, BuildRepository>();
            services.AddScoped<IHashedEntityRepository<Currency>, CurrencyRepository>();
            services.AddScoped<IHashedEntityRepository<ElementalTypeRelation>, ElementalTypeRelationRepository>();
            services.AddScoped<IHashedEntityRepository<ElementalType>, ElementalTypeRepository>();
            services.AddScoped<IHashedEntityRepository<Event>, EventRepository>();
            services.AddScoped<IHashedEntityRepository<Evolution>, EvolutionRepository>();
            services.AddScoped<IHashedEntityRepository<HuntingConfiguration>, HuntingConfigurationRepository>();
            services.AddScoped<IHashedEntityRepository<Item>, ItemRepository>();
            services.AddScoped<IHashedEntityRepository<ItemStatBoostPokemon>, ItemStatBoostPokemonRepository>();
            services.AddScoped<IHashedEntityRepository<LearnableMoveLearnMethod>, LearnableMoveLearnMethodRepository>();
            services.AddScoped<IHashedEntityRepository<Location>, LocationRepository>();
            services.AddScoped<IHashedEntityRepository<MoveDamageClass>, MoveDamageClassRepository>();
            services.AddScoped<IHashedEntityRepository<MoveLearnMethodLocation>, MoveLearnMethodLocationRepository>();
            services.AddScoped<IHashedEntityRepository<Move>, MoveRepository>();
            services.AddScoped<IHashedEntityRepository<MoveTutorMove>, MoveTutorMoveRepository>();
            services.AddScoped<IHashedEntityRepository<MoveTutor>, MoveTutorRepository>();
            services.AddScoped<IHashedEntityRepository<Nature>, NatureRepository>();
            services.AddScoped<IHashedEntityRepository<PlacedItem>, PlacedItemRepository>();
            services.AddScoped<IHashedEntityRepository<PokemonForm>, PokemonFormRepository>();
            services.AddScoped<IHashedEntityRepository<PvpTier>, PvpTierRepository>();
            services.AddScoped<IHashedEntityRepository<Region>, RegionRepository>();
            services.AddScoped<IHashedEntityRepository<Season>, SeasonRepository>();
            services.AddScoped<IHashedEntityRepository<SeasonTimeOfDay>, SeasonTimeOfDayRepository>();
            services.AddScoped<IHashedEntityRepository<Spawn>, SpawnRepository>();
            services.AddScoped<IHashedEntityRepository<SpawnType>, SpawnTypeRepository>();
            services.AddScoped<IHashedEntityRepository<TimeOfDay>, TimeOfDayRepository>();

            services.AddScoped<IReadModelUpdateService, JsonReadModelUpdateService>();

            services.AddScoped<IReadModelMapper<EntityTypeReadModel>, EntityTypeReadModelMapper>();
            services.AddScoped<IReadModelMapper<ItemStatBoostPokemonReadModel>, ItemStatBoostPokemonReadModelMapper>();
            services.AddScoped<IReadModelMapper<SimpleLearnableMoveReadModel>, SimpleLearnableMoveReadModelMapper>();
            services.AddScoped<IReadModelMapper<MoveReadModel>, MoveReadModelMapper>();
            services.AddScoped<IReadModelMapper<NatureReadModel>, NatureReadModelMapper>();
            services.AddScoped<IReadModelMapper<PokemonVarietyReadModel>, PokemonReadModelMapper>();
            services.AddScoped<IReadModelMapper<ItemReadModel>, ItemReadModelMapper>();
            services.AddScoped<IReadModelMapper<RegionReadModel>, RegionReadModelMapper>();
            services.AddScoped<IReadModelMapper<LocationGroupReadModel>, LocationGroupReadModelMapper>();
        }
    }
}