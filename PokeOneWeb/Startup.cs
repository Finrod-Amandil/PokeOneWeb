using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.WebApi.Services.Api;
using PokeOneWeb.WebApi.Services.Api.Impl;
=======
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PokeOneWeb.Configuration;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Services.DataUpdate;
using PokeOneWeb.Services.DataUpdate.Impl;
using PokeOneWeb.Services.GoogleSpreadsheet;
using PokeOneWeb.Services.GoogleSpreadsheet.Impl;
using PokeOneWeb.Services.GoogleSpreadsheet.Impl.LearnableMoveLearnMethods;
using PokeOneWeb.Services.GoogleSpreadsheet.Impl.Locations;
using PokeOneWeb.Services.GoogleSpreadsheet.Impl.PlacedItems;
using PokeOneWeb.Services.GoogleSpreadsheet.Impl.Spawns;
using PokeOneWeb.Services.GoogleSpreadsheet.Impl.TutorMoves;
using PokeOneWeb.Services.PokeApi;
using PokeOneWeb.Services.PokeApi.Impl;
>>>>>>> e9c6583 (Bulk commit - model refinements, migrations, dataupdateservices, spreasheet import)

namespace PokeOneWeb.WebApi
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
<<<<<<< HEAD
            ConfigureDatabases(services);

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddDebug();
            });

            services.AddControllers();

            services.AddScoped<IEntityTypeApiService, EntityTypeApiService>();
            services.AddScoped<IItemApiService, ItemApiService>();
            services.AddScoped<IMoveApiService, MoveApiService>();
            services.AddScoped<INatureApiService, NatureApiService>();
            services.AddScoped<IPokemonApiService, PokemonApiService>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowed(host => true));
            });
=======
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.Configure<PokeApiSettings>(options => Configuration.GetSection("PokeApiSettings").Bind(options));
            
            services.AddTransient<IPokeApiService, PokeApiService>();
            services.AddTransient<IGoogleSpreadsheetService, GoogleSpreadsheetService>();
            services.AddTransient<ISpreadsheetLoader, SpreadsheetLoader>();

            services.AddTransient<ISpreadsheetReader<LocationDto>, LocationReader>();
            services.AddTransient<ISpreadsheetReader<PlacedItemDto>, PlacedItemReader>();
            services.AddTransient<ISpreadsheetReader<SpawnDto>, SpawnReader>();
            services.AddTransient<ISpreadsheetReader<TutorMoveDto>, TutorMoveReader>();
            services.AddTransient<ISpreadsheetReader<LearnableMoveLearnMethodDto>, LearnableMoveLearnMethodReader>();

            services.AddTransient<ISpreadsheetMapper<LocationDto, Location>, LocationMapper>();
            services.AddTransient<ISpreadsheetMapper<PlacedItemDto, PlacedItem>, PlacedItemMapper>();
            services.AddTransient<ISpreadsheetMapper<SpawnDto, Spawn>, SpawnMapper>();
            services.AddTransient<ISpreadsheetMapper<TutorMoveDto, TutorMove>, TutorMoveMapper>();
            services.AddTransient<ISpreadsheetMapper<LearnableMoveLearnMethodDto, LearnableMove>, LearnableMoveLearnMethodMapper>();

            services.AddTransient<IDataUpdateService<Location>, LocationUpdateService>();
            services.AddTransient<IDataUpdateService<Spawn>, SpawnUpdateService>();
            services.AddTransient<IDataUpdateService<PlacedItem>, PlacedItemUpdateService>();
            services.AddTransient<IDataUpdateService<TutorMove>, TutorMoveUpdateService>();
            services.AddTransient<IDataUpdateService<LearnableMove>, LearnableMoveUpdateService>();
>>>>>>> e9c6583 (Bulk commit - model refinements, migrations, dataupdateservices, spreasheet import)
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}");
            });
        }

        public void ConfigureDatabases(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                options.ConfigureWarnings(w => w.Throw(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
            });

            services.AddDbContext<ReadModelDbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("ReadModelConnection"),
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                options.ConfigureWarnings(w => w.Throw(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
            });

            services.AddDatabaseDeveloperPageExceptionFilter();
        }
    }
}
