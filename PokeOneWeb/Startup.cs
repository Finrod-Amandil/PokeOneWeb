using System.Threading.Tasks;
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
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.ItemStatBoosts;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Moves;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Natures;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Pokemon;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.PvpTiers;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.SpawnTypes;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Times;
using PokeOneWeb.Services.PokeApi;
using PokeOneWeb.Services.PokeApi.Impl;
using PokeOneWeb.Services.ReadModelUpdate;
using PokeOneWeb.Services.ReadModelUpdate.Impl;

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
            services.Configure<PokeApiSettings>(options => Configuration.GetSection("PokeApiSettings").Bind(options));

            services.AddScoped<IPokeApiService, PokeApiService>();
            services.AddScoped<IGoogleSpreadsheetImportService, GoogleSpreadsheetImportService>();
            services.AddScoped<ISpreadsheetLoader, SpreadsheetLoader>();

            services.AddScoped<ISpreadsheetEntityReader<RegionDto>, RegionReader>();
            services.AddScoped<ISpreadsheetEntityReader<LocationDto>, LocationReader>();
            services.AddScoped<ISpreadsheetEntityReader<PlacedItemDto>, PlacedItemReader>();
            services.AddScoped<ISpreadsheetEntityReader<SpawnDto>, SpawnReader>();
            services.AddScoped<ISpreadsheetEntityReader<TutorMoveDto>, TutorMoveReader>();
            services.AddScoped<ISpreadsheetEntityReader<LearnableMoveLearnMethodDto>, LearnableMoveLearnMethodReader>();
            services.AddScoped<ISpreadsheetEntityReader<BuildDto>, BuildReader>();
            services.AddScoped<ISpreadsheetEntityReader<HuntingConfigurationDto>, HuntingConfigurationReader>();
            services.AddScoped<ISpreadsheetEntityReader<ItemDto>, ItemReader>();
            services.AddScoped<ISpreadsheetEntityReader<ItemStatBoostDto>, ItemStatBoostReader>();
            services.AddScoped<ISpreadsheetEntityReader<AbilityDto>, AbilityReader>();
            services.AddScoped<ISpreadsheetEntityReader<ElementalTypeDto>, ElementalTypeReader>();
            services.AddScoped<ISpreadsheetEntityReader<ElementalTypeRelationDto>, ElementalTypeRelationReader>();
            services.AddScoped<ISpreadsheetEntityReader<MoveDto>, MoveReader>();
            services.AddScoped<ISpreadsheetEntityReader<NatureDto>, NatureReader>();
            services.AddScoped<ISpreadsheetEntityReader<TimeDto>, TimeReader>();
            services.AddScoped<ISpreadsheetEntityReader<CurrencyDto>, CurrencyReader>();
            services.AddScoped<ISpreadsheetEntityReader<AvailabilityDto>, AvailabilityReader>();
            services.AddScoped<ISpreadsheetEntityReader<PvpTierDto>, PvpTierReader>();
            services.AddScoped<ISpreadsheetEntityReader<PokemonDto>, PokemonReader>();
            services.AddScoped<ISpreadsheetEntityReader<EvolutionDto>, EvolutionReader>();
            services.AddScoped<ISpreadsheetEntityReader<SpawnTypeDto>, SpawnTypeReader>();

            services.AddScoped<ISpreadsheetEntityMapper<RegionDto, Region>, RegionMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<LocationDto, Location>, LocationMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<PlacedItemDto, PlacedItem>, PlacedItemMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<SpawnDto, Spawn>, SpawnMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<TutorMoveDto, TutorMove>, TutorMoveMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<LearnableMoveLearnMethodDto, LearnableMove>, LearnableMoveLearnMethodMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<BuildDto, Build>, BuildMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<HuntingConfigurationDto, HuntingConfiguration>, HuntingConfigurationMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<ItemDto, Item>, ItemMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<ItemStatBoostDto, ItemStatBoost>, ItemStatBoostMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<AbilityDto, Ability>, AbilityMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<ElementalTypeDto, ElementalType>, ElementalTypeMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<ElementalTypeRelationDto, ElementalTypeRelation>, ElementalTypeRelationMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<MoveDto, Move>, MoveMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<NatureDto, Nature>, NatureMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<TimeDto, SeasonTimeOfDay>, TimeMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<CurrencyDto, Currency>, CurrencyMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<AvailabilityDto, PokemonAvailability>, AvailabilityMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<PvpTierDto, PvpTier>, PvpTierMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<PokemonDto, PokemonForm>, PokemonMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<EvolutionDto, Evolution>, EvolutionMapper>();
            services.AddScoped<ISpreadsheetEntityMapper<SpawnTypeDto, SpawnType>, SpawnTypeMapper>();

            services.AddScoped<ISpreadsheetEntityImporter<RegionDto, Region>, RegionImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<LocationDto, Location>, LocationImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<SpawnDto, Spawn>, SpawnImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<PlacedItemDto, PlacedItem>, PlacedItemImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<TutorMoveDto, TutorMove>, TutorMoveImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<LearnableMoveLearnMethodDto, LearnableMove>, LearnableMoveImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<BuildDto, Build>, BuildImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<HuntingConfigurationDto, HuntingConfiguration>, HuntingConfigurationImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<ItemDto, Item>, ItemImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<ItemStatBoostDto, ItemStatBoost>, ItemStatBoostImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<AbilityDto, Ability>, AbilityImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<ElementalTypeDto, ElementalType>, ElementalTypeImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<ElementalTypeRelationDto, ElementalTypeRelation>, ElementalTypeRelationImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<MoveDto, Move>, MoveImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<NatureDto, Nature>, NatureImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<TimeDto, SeasonTimeOfDay>, TimeImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<CurrencyDto, Currency>, CurrencyImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<AvailabilityDto, PokemonAvailability>, AvailabilityImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<PvpTierDto, PvpTier>, PvpTierImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<PokemonDto, PokemonForm>, PokemonImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<EvolutionDto, Evolution>, EvolutionImporter>();
            services.AddScoped<ISpreadsheetEntityImporter<SpawnTypeDto, SpawnType>, SpawnTypeImporter>();

            services.AddScoped<IReadModelUpdateService, ReadModelUpdateService>();
            services.AddScoped<IReadModelMapper<PokemonReadModel>, PokemonReadModelMapper>();
            services.AddScoped<IReadModelMapper<MoveReadModel>, MoveReadModelMapper>();
            services.AddScoped<IReadModelMapper<SimpleLearnableMoveReadModel>, SimpleLearnableMoveReadModelMapper>();
            services.AddScoped<IReadModelMapper<EntityTypeReadModel>, EntityTypeReadModelMapper>();
            services.AddScoped<IReadModelMapper<ItemStatBoostReadModel>, ItemStatBoostReadModelMapper>();
            services.AddScoped<IReadModelMapper<NatureReadModel>, NatureReadModelMapper>();

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
