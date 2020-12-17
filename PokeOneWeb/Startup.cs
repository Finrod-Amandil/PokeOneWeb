using System.Threading.Tasks;
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

            services.AddTransient<IReadModelUpdateService, ReadModelUpdateService>();
            services.AddTransient<IReadModelMapper<PokemonReadModel>, PokemonReadModelMapper>();
            services.AddTransient<IReadModelMapper<MoveReadModel>, MoveReadModelMapper>();
            services.AddTransient<IReadModelMapper<SimpleLearnableMoveReadModel>, SimpleLearnableMoveReadModelMapper>();

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
            readModelUpdateService.UpdateReadModel();
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
