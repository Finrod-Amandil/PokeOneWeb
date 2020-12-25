using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddDbContext<ReadModelDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ReadModelConnection")));

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.Configure<PokeApiSettings>(options => Configuration.GetSection("PokeApiSettings").Bind(options));

            services.AddTransient<IPokeApiService, PokeApiService>();
            services.AddTransient<IGoogleSpreadsheetImportService, GoogleSpreadsheetImportService>();
            services.AddTransient<ISpreadsheetLoader, SpreadsheetLoader>();

            services.AddTransient<ISpreadsheetEntityReader<RegionDto>, RegionReader>();
            services.AddTransient<ISpreadsheetEntityReader<LocationDto>, LocationReader>();
            services.AddTransient<ISpreadsheetEntityReader<PlacedItemDto>, PlacedItemReader>();
            services.AddTransient<ISpreadsheetEntityReader<SpawnDto>, SpawnReader>();
            services.AddTransient<ISpreadsheetEntityReader<TutorMoveDto>, TutorMoveReader>();
            services.AddTransient<ISpreadsheetEntityReader<LearnableMoveLearnMethodDto>, LearnableMoveLearnMethodReader>();
            services.AddTransient<ISpreadsheetEntityReader<BuildDto>, BuildReader>();
            services.AddTransient<ISpreadsheetEntityReader<HuntingConfigurationDto>, HuntingConfigurationReader>();
            services.AddTransient<ISpreadsheetEntityReader<ItemDto>, ItemReader>();
            services.AddTransient<ISpreadsheetEntityReader<AbilityDto>, AbilityReader>();
            services.AddTransient<ISpreadsheetEntityReader<ElementalTypeDto>, ElementalTypeReader>();
            services.AddTransient<ISpreadsheetEntityReader<ElementalTypeRelationDto>, ElementalTypeRelationReader>();
            services.AddTransient<ISpreadsheetEntityReader<MoveDto>, MoveReader>();
            services.AddTransient<ISpreadsheetEntityReader<NatureDto>, NatureReader>();
            services.AddTransient<ISpreadsheetEntityReader<TimeDto>, TimeReader>();
            services.AddTransient<ISpreadsheetEntityReader<CurrencyDto>, CurrencyReader>();
            services.AddTransient<ISpreadsheetEntityReader<AvailabilityDto>, AvailabilityReader>();
            services.AddTransient<ISpreadsheetEntityReader<PvpTierDto>, PvpTierReader>();
            services.AddTransient<ISpreadsheetEntityReader<PokemonDto>, PokemonReader>();
            services.AddTransient<ISpreadsheetEntityReader<EvolutionDto>, EvolutionReader>();
            services.AddTransient<ISpreadsheetEntityReader<SpawnTypeDto>, SpawnTypeReader>();

            services.AddTransient<ISpreadsheetEntityMapper<RegionDto, Region>, RegionMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<LocationDto, Location>, LocationMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<PlacedItemDto, PlacedItem>, PlacedItemMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<SpawnDto, Spawn>, SpawnMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<TutorMoveDto, TutorMove>, TutorMoveMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<LearnableMoveLearnMethodDto, LearnableMove>, LearnableMoveLearnMethodMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<BuildDto, Build>, BuildMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<HuntingConfigurationDto, HuntingConfiguration>, HuntingConfigurationMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<ItemDto, Item>, ItemMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<AbilityDto, Ability>, AbilityMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<ElementalTypeDto, ElementalType>, ElementalTypeMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<ElementalTypeRelationDto, ElementalTypeRelation>, ElementalTypeRelationMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<MoveDto, Move>, MoveMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<NatureDto, Nature>, NatureMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<TimeDto, SeasonTimeOfDay>, TimeMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<CurrencyDto, Currency>, CurrencyMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<AvailabilityDto, PokemonAvailability>, AvailabilityMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<PvpTierDto, PvpTier>, PvpTierMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<PokemonDto, PokemonForm>, PokemonMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<EvolutionDto, Evolution>, EvolutionMapper>();
            services.AddTransient<ISpreadsheetEntityMapper<SpawnTypeDto, SpawnType>, SpawnTypeMapper>();

            services.AddTransient<ISpreadsheetEntityImporter<RegionDto, Region>, RegionImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<LocationDto, Location>, LocationImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<SpawnDto, Spawn>, SpawnImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<PlacedItemDto, PlacedItem>, PlacedItemImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<TutorMoveDto, TutorMove>, TutorMoveImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<LearnableMoveLearnMethodDto, LearnableMove>, LearnableMoveImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<BuildDto, Build>, BuildImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<HuntingConfigurationDto, HuntingConfiguration>, HuntingConfigurationImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<ItemDto, Item>, ItemImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<AbilityDto, Ability>, AbilityImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<ElementalTypeDto, ElementalType>, ElementalTypeImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<ElementalTypeRelationDto, ElementalTypeRelation>, ElementalTypeRelationImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<MoveDto, Move>, MoveImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<NatureDto, Nature>, NatureImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<TimeDto, SeasonTimeOfDay>, TimeImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<CurrencyDto, Currency>, CurrencyImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<AvailabilityDto, PokemonAvailability>, AvailabilityImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<PvpTierDto, PvpTier>, PvpTierImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<PokemonDto, PokemonForm>, PokemonImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<EvolutionDto, Evolution>, EvolutionImporter>();
            services.AddTransient<ISpreadsheetEntityImporter<SpawnTypeDto, SpawnType>, SpawnTypeImporter>();

            services.AddTransient<IReadModelUpdateService, ReadModelUpdateService>();
            services.AddTransient<IReadModelMapper<PokemonReadModel>, PokemonReadModelMapper>();
            services.AddTransient<IReadModelMapper<MoveReadModel>, MoveReadModelMapper>();
            services.AddTransient<IReadModelMapper<SimpleLearnableMoveReadModel>, SimpleLearnableMoveReadModelMapper>();
            services.AddTransient<IReadModelMapper<EntityTypeReadModel>, EntityTypeReadModelMapper>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowed(host => true));
            });

            //Update ReadModel on StartUp
            var serviceProvider = services.BuildServiceProvider();
            var readModelUpdateService = serviceProvider.GetService<IReadModelUpdateService>();
            //Task.Run(() => readModelUpdateService.UpdateReadModel());
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
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
