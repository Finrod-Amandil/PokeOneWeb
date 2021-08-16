using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Configuration;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.Services.GoogleSpreadsheet.Import;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Abilities;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Availabilities;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.BagCategories;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Builds;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Currencies;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.ElementalTypeRelations;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.ElementalTypes;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Events;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Evolutions;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.HuntingConfigurations;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Items;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.ItemStatBoosts;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.LearnableMoveLearnMethods;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Locations;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.Builds;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MoveDamageClasses;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MoveLearnMethodLocations;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Moves;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MoveTutorMoves;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MoveTutors;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Natures;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.PlacedItems;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Pokemon;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.PvpTiers;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Regions;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Seasons;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.SeasonTimesOfDay;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Spawns;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.SpawnTypes;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.TimesOfDay;
using PokeOneWeb.Services.PokeApi;
using PokeOneWeb.Services.PokeApi.Impl;
using PokeOneWeb.Services.ReadModelUpdate;
using PokeOneWeb.Services.ReadModelUpdate.Impl;
using PokeOneWeb.Services.ReadModelUpdate.Impl.EntityTypes;
using PokeOneWeb.Services.ReadModelUpdate.Impl.ItemStatBoostPokemon;
using PokeOneWeb.Services.ReadModelUpdate.Impl.LearnableMoves;
using PokeOneWeb.Services.ReadModelUpdate.Impl.Moves;
using PokeOneWeb.Services.ReadModelUpdate.Impl.Natures;
using PokeOneWeb.Services.ReadModelUpdate.Impl.Pokemon;
using Ability = PokeOneWeb.Data.Entities.Ability;
using Item = PokeOneWeb.Data.Entities.Item;
using Location = PokeOneWeb.Data.Entities.Location;
using Move = PokeOneWeb.Data.Entities.Move;
using MoveDamageClass = PokeOneWeb.Data.Entities.MoveDamageClass;
using Nature = PokeOneWeb.Data.Entities.Nature;
using PokemonForm = PokeOneWeb.Data.Entities.PokemonForm;

namespace PokeOneWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                //options.EnableSensitiveDataLogging();
            });
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddDbContext<ReadModelDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ReadModelConnection")));

            services.AddLogging(loggingBuilder =>
            {
                //loggingBuilder.AddConsole().AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
                loggingBuilder.AddDebug();
            });

            services.AddControllersWithViews();
            services.AddRazorPages();

            // Configuration sections
            services.Configure<PokeApiSettings>(options => 
                Configuration.GetSection("PokeApiSettings").Bind(options));
            services.Configure<GoogleSpreadsheetsSettings>(options =>
                Configuration.GetSection("GoogleSpreadsheetsSettings").Bind(options));

            services.AddScoped<IPokeApiService, PokeApiService>();

            services.AddScoped<IGoogleSpreadsheetImportService, GoogleSpreadsheetImportService>();
            services.AddScoped<IHashListComparator, HashListComparator>();
            services.AddScoped<ISheetNameHelper, SheetNameHelper>();
            services.AddScoped<ISpreadsheetDataLoader, SpreadsheetDataLoader>();

            services.AddScoped<ISheetRowParser<AbilityDto>, AbilitySheetRowParser>();
            services.AddScoped<ISheetRowParser<AvailabilityDto>, AvailabilitySheetRowParser>();
            services.AddScoped<ISheetRowParser<BagCategoryDto>, BagCategorySheetRowParser>();
            services.AddScoped<ISheetRowParser<BuildDto>, BuildSheetRowParser>();
            services.AddScoped<ISheetRowParser<CurrencyDto>, CurrencySheetRowParser>();
            services.AddScoped<ISheetRowParser<ElementalTypeRelationDto>, ElementalTypeRelationSheetRowParser>();
            services.AddScoped<ISheetRowParser<ElementalTypeDto>, ElementalTypeSheetRowParser>();
            services.AddScoped<ISheetRowParser<EventDto>, EventSheetRowParser>();
            services.AddScoped<ISheetRowParser<EvolutionDto>, EvolutionSheetRowParser>();
            services.AddScoped<ISheetRowParser<HuntingConfigurationDto>, HuntingConfigurationSheetRowParser>();
            services.AddScoped<ISheetRowParser<ItemDto>, ItemSheetRowParser>();
            services.AddScoped<ISheetRowParser<ItemStatBoostDto>, ItemStatBoostSheetRowParser>();
            services.AddScoped<ISheetRowParser<LearnableMoveLearnMethodDto>, LearnableMoveLearnMethodSheetRowParser>();
            services.AddScoped<ISheetRowParser<LocationDto>, LocationSheetRowParser>();
            services.AddScoped<ISheetRowParser<MoveDamageClassDto>, MoveDamageClassSheetRowParser>();
            services.AddScoped<ISheetRowParser<MoveLearnMethodLocationDto>, MoveLearnMethodLocationSheetRowParser>();
            services.AddScoped<ISheetRowParser<MoveDto>, MoveSheetRowParser>();
            services.AddScoped<ISheetRowParser<MoveTutorMoveDto>, MoveTutorMoveSheetRowParser>();
            services.AddScoped<ISheetRowParser<MoveTutorDto>, MoveTutorSheetRowParser>();
            services.AddScoped<ISheetRowParser<NatureDto>, NatureSheetRowParser>();
            services.AddScoped<ISheetRowParser<PlacedItemDto>, PlacedItemSheetRowParser>();
            services.AddScoped<ISheetRowParser<PokemonDto>, PokemonSheetRowParser>();
            services.AddScoped<ISheetRowParser<PvpTierDto>, PvpTierSheetRowParser>();
            services.AddScoped<ISheetRowParser<RegionDto>, RegionSheetRowParser>();
            services.AddScoped<ISheetRowParser<SeasonDto>, SeasonSheetRowParser>();
            services.AddScoped<ISheetRowParser<SeasonTimeOfDayDto>, SeasonTimeOfDaySheetRowParser>();
            services.AddScoped<ISheetRowParser<SpawnDto>, SpawnSheetRowParser>();
            services.AddScoped<ISheetRowParser<SpawnTypeDto>, SpawnTypeSheetRowParser>();
            services.AddScoped<ISheetRowParser<TimeOfDayDto>, TimeOfDaySheetRowParser>();

            services.AddScoped<ISpreadsheetEntityMapper<AbilityDto, Ability>, AbilityMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<AvailabilityDto, PokemonAvailability>, AvailabilityMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<BagCategoryDto, BagCategory>, BagCategoryMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<BuildDto, Build>, BuildMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<CurrencyDto, Currency>, CurrencyMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<ElementalTypeRelationDto, ElementalTypeRelation>, ElementalTypeRelationMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<ElementalTypeDto, ElementalType>, ElementalTypeMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<EventDto, Event>, EventMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<EvolutionDto, Evolution>, EvolutionMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<HuntingConfigurationDto, HuntingConfiguration>, HuntingConfigurationMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<ItemDto, Item>, ItemMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<ItemStatBoostDto, ItemStatBoostPokemon>, ItemStatBoostMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<LearnableMoveLearnMethodDto, LearnableMoveLearnMethod>, LearnableMoveLearnMethodMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<LocationDto, Location>, LocationMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<MoveDamageClassDto, MoveDamageClass>, MoveDamageClassMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<MoveLearnMethodLocationDto, MoveLearnMethodLocation>, MoveLearnMethodLocationMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<MoveDto, Move>, MoveMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<MoveTutorMoveDto, MoveTutorMove>, MoveTutorMoveMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<MoveTutorDto, MoveTutor>, MoveTutorMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<NatureDto, Nature>, NatureMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<PlacedItemDto, PlacedItem>, PlacedItemMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<PokemonDto, PokemonForm>, PokemonMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<PvpTierDto, PvpTier>, PvpTierMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<RegionDto, Region>, RegionMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<SeasonDto, Season>, SeasonMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<SeasonTimeOfDayDto, SeasonTimeOfDay>, SeasonTimeOfDayMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<SpawnDto, Spawn>, SpawnMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<SpawnTypeDto, SpawnType>, SpawnTypeMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<TimeOfDayDto, TimeOfDay>, TimeOfDayMapper>();

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

            services.AddScoped<IReadModelRepository<EntityTypeReadModel>, EntityTypeReadModelRepository>();
            services.AddScoped<IReadModelRepository<ItemStatBoostPokemonReadModel>, ItemStatBoostPokemonReadModelRepository>();
            services.AddScoped<IReadModelRepository<SimpleLearnableMoveReadModel>, SimpleLearnableMoveReadModelRepository>();
            services.AddScoped<IReadModelRepository<MoveReadModel>, MoveReadModelRepository>();
            services.AddScoped<IReadModelRepository<NatureReadModel>, NatureReadModelRepository>();
            services.AddScoped<IReadModelRepository<PokemonVarietyReadModel>, PokemonVarietyReadModelRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowed(host => true));
            });

            //Update ReadModel on StartUp
            /*var serviceProvider = services.BuildServiceProvider();
            var readModelUpdateService = serviceProvider.GetService<IReadModelUpdateService>();
            Task.Run(() => readModelUpdateService.UpdateReadModel());/**/
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
                endpoints.MapRazorPages();
            });
        }
    }
}
