using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PokeOneWeb.Services.PokeApi;
using PokeOneWeb.Services.PokeApi.Impl;
using PokeOneWeb.Configuration;
using PokeOneWeb.Services.GoogleSpreadsheet;
using PokeOneWeb.Services.GoogleSpreadsheet.Impl;
using PokeOneWeb.Services.GoogleSpreadsheet.Impl.Location;
using PokeOneWeb.Services.GoogleSpreadsheet.Impl.Spawn;
using PokeOneWeb.Services.GoogleSpreadsheet.Impl.PlacedItem;
using PokeOneWeb.Data.Entities;

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
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.Configure<PokeApiSettings>(options => Configuration.GetSection("PokeApiSettings").Bind(options));
            
            services.AddTransient<IPokeApiService, PokeApiService>();
            services.AddTransient<IGoogleSpreadsheetService, GoogleSpreadsheetService>();
            services.AddTransient<ISpreadsheetLoader, SpreadsheetLoader>();
            services.AddTransient<ISpreadsheetReader<LocationDto>, LocationReader>();
            services.AddTransient<ISpreadsheetReader<PlacedItemDto>, PlacedItemReader>();
            services.AddTransient<ISpreadsheetReader<SpawnDto>, SpawnReader>();
            services.AddTransient<ISpreadsheetMapper<LocationDto, Location>, LocationMapper>();
            services.AddTransient<ISpreadsheetMapper<PlacedItemDto, PlacedItem>, PlacedItemMapper>();
            services.AddTransient<ISpreadsheetMapper<SpawnDto, Spawn>, SpawnMapper>();

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
